using System;
using System.Web;
using System.Globalization;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Configuration;
using System.Web.Util;
using System.Security.Permissions;
using System.Web.Hosting;
using System.Security.Principal;
using System.Security;
using System.Diagnostics;
using System.Web.Configuration;

namespace LMS.MembershipController
{
    /// <summary>
    /// Summary description for MyConnectionHelper
    /// </summary>
    internal static class MyConnectionHelper
    {
        private const string s_connPrefix = "Server=.\\SQLExpress; Database=UsersDb1;Trusted_Connection=Yes;AttachDbFilename=|Datadirectory|";
        static private Hashtable _Connections = Hashtable.Synchronized(new Hashtable(StringComparer.InvariantCultureIgnoreCase));

        /////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////
        private static void BuildConnectionForFileName(string dbFileName)
        {
            /////////////////////////////////////////////
            // Step 0: Check if connection already exists
            if (_Connections[dbFileName] != null)
                return;
            /////////////////////////////////////////////
            // Step 1: Check if it is a valid connection string
            bool isConnString = false;
            SqlConnection conn = null;

            if (dbFileName.IndexOf(';') >= 0 && dbFileName.IndexOf('=') >= 0)
            { // Is probably a connection string
                try
                {
                    conn = new SqlConnection(dbFileName);
                    try
                    {
                        conn.Open();
                        isConnString = true;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                catch
                {
                    isConnString = false;
                }
            }

            if (isConnString)
            {
                _Connections.Add(dbFileName, new AccessConnectionHolder(conn));
                return;
            }

            ////////////////////////////////////////////////////////////////////
            // Step 2: Check is it's a full path: use as-is, if it is a full path
            //if (Path.IsPathRooted(dbFileName))
            //{
            //    EnsureValidMdbFile(dbFileName);
            //    _Connections.Add(dbFileName, new AccessConnectionHolder(new SqlConnection(s_connPrefix + dbFileName)));
            //    return;
            //}

            ////////////////////////////////////////////////////////////
            // Step 3: Ensure that it doesn't try to walk up a directory
            if (dbFileName.Contains(".."))
            {
                throw new Exception("File name can not contain dot dot(..): " + dbFileName);
            }

            ////////////////////////////////////////////////////////////
            // Step 4: Get the full path for this (relative) filename
            string filename = GetFullPathNameFromDBFileName(dbFileName);

            ////////////////////////////////////////////////////////////
            // Step 5: Create and add connection
            EnsureValidMdbFile(filename);
            _Connections.Add(dbFileName, new AccessConnectionHolder(new SqlConnection(s_connPrefix + filename)));
        }



        /////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////
        internal static AccessConnectionHolder GetConnection(string dbFileName, bool revertImpersonation)
        {
            dbFileName = dbFileName.Trim();

            /////////////////////////////////////////////////
            // Lock the connections table, and see if it already exists
            lock (_Connections)
            {
                AccessConnectionHolder holder = (AccessConnectionHolder)_Connections[dbFileName];
                if (holder != null && !File.Exists(holder.Connection.DataSource))
                {
                    _Connections.Remove(dbFileName);
                    holder = null;
                }
                if (holder == null)
                {
                    BuildConnectionForFileName(dbFileName);
                    holder = (AccessConnectionHolder)_Connections[dbFileName];
                }
                if (holder == null)
                {
                    return null;
                }
                holder.Open(null);
                return holder;
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////
        private static string GetFullPathNameFromDBFileName(string relativeFileName)
        {
            relativeFileName = relativeFileName.Replace('/', '\\'); // replace / with \
            if (relativeFileName.StartsWith("~\\"))
                relativeFileName = relativeFileName.Substring(2);
            else if (relativeFileName.StartsWith("\\"))
                relativeFileName = relativeFileName.Substring(1);
            //return Path.Combine(HttpRuntime.AppDomainAppPath, relativeFileName);
            return(HttpRuntime.AppDomainAppPath + relativeFileName);
        }

        /////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////
        private static void EnsureValidMdbFile(string fileName)
        {
            SqlConnection conn = null;
            string constrrr = s_connPrefix + fileName;
            try
            {
                conn = new SqlConnection(constrrr);
                conn.Open();
            }
            catch(Exception ex)
            {
                throw new Exception("AccessFile is not valid: ",ex);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////
        internal static string GetFileNameFromConnectionName(string connectionName, bool appLevel)
        {
            ConnectionStringSettings connObj = ConfigurationManager.ConnectionStrings[connectionName];
            if (connObj != null)
            {
                return connObj.ConnectionString;
            }

            return null;
        }

        /////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////
        internal static Exception GetBetterException(Exception e, AccessConnectionHolder holder)
        {
            try
            {
                if (!(e is SqlException) || holder.Connection == null ||
                    holder.Connection.DataSource == null || holder.Connection.DataSource.Length < 1)
                {
                    return e;
                }
                if (!File.Exists(holder.Connection.DataSource))
                {
                    return new FileNotFoundException(String.Empty, holder.Connection.DataSource, e);
                }
            }
            finally
            {
                if (holder.Connection != null)
                    holder.Connection.Close();
            }

            FileStream s = null;
            Exception eWrite = null;
            try
            {
                s = File.OpenWrite(holder.Connection.DataSource);
            }
            catch (Exception except)
            {
                eWrite = except;
            }
            finally
            {
                if (s != null)
                    s.Close();
            }
            if (eWrite != null && (eWrite is UnauthorizedAccessException))
            {
                HttpContext context = HttpContext.Current;
                if (context != null)
                {
                    context.Response.Clear();
                    context.Response.StatusCode = 500;
                    context.Response.Write("Cannot write to DB File");
                    context.Response.End();
                }
                return new Exception("AccessFile is not writtable", eWrite);
            }
            return e;
        }

        internal static void CheckConnectionString(string fileName)
        {
            if (fileName.IndexOf(';') >= 0 && fileName.IndexOf('=') >= 0) // Is probably a connection string
                return;
            if (Path.IsPathRooted(fileName))
            { // Full path
                if (!File.Exists(fileName))
                    throw new Exception("$safeprojectname$ File not found: " + fileName);
                return;
            }
            char c = fileName[0];
            if (c == '/' || c == '\\')
            {
                throw new Exception("$safeprojectname$ File can not start with this char: " + c);
            }
            if (fileName.Contains(".."))
            {
                throw new Exception("File name can not contain '..': " + fileName);
            }
        }

        internal static DateTime RoundToSeconds(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }

        internal static int GetUserID(SqlConnection connection, int applicationID, string userName, bool createIfNeeded)
        {
            if (applicationID == 0 || userName == null || userName.Length < 1) // Application doesn't exist or user doesn't exist
                return 0;

            if (connection == null)
                return 0; // Wrong params!

            SqlCommand lookupCommand = new SqlCommand(@"SELECT UserId FROM Users WHERE UserName = @UserName",
                                                      connection);
            lookupCommand.Parameters.Add(new SqlParameter("@UserName", userName));

            object lookupResult = lookupCommand.ExecuteScalar();
            if ((lookupResult != null) && (lookupResult is int))
            {
                return (int)lookupResult;
            }

            if (createIfNeeded)
            {
                SqlCommand createCommand = new SqlCommand(@"INSERT INTO Users " +
                                                          @"(UserName) " +
                                                          @"VALUES (@UserName)",
                                                          connection);
                createCommand.Parameters.Add(new SqlParameter("@UserName", userName));

                if (createCommand.ExecuteNonQuery() != 0)
                {
                    lookupResult = lookupCommand.ExecuteScalar();
                    if ((lookupResult != null) && (lookupResult is int))
                    {
                        return (int)lookupResult;
                    }
                }
            }

            return 0;
        }
    }

    /// //////////////////////////////////////////////////////////////////////////////
    internal sealed class AccessConnectionHolder
    {
        internal SqlConnection Connection;
        private bool _Opened;
        internal DateTime CreateDate;

        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////
        internal AccessConnectionHolder(SqlConnection connection)
        {
            Connection = connection;
            CreateDate = DateTime.Now;
        }

        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////
        internal void Open(HttpContext context)
        {
            //////////////////////////////////////////
            // Step 1: Get Exclusive lock
            Monitor.Enter(this);
            if (_Opened)
                return; // Already opened

            //////////////////////////////////////////
            // Step 3: Open connection
            try
            {
                Connection.Open();
            }
            catch
            {
                // remove exclusive lock
                Monitor.Exit(this);
                throw; // re-throw the exception
            }

            _Opened = true; // Open worked!
        }

        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////
        internal void Close()
        {
            if (!_Opened) // Not open!
                return;

            // Close connection
            Connection.Close();

            _Opened = false;

            // Remove exclusive access
            Monitor.Exit(this);
        }
    }
}