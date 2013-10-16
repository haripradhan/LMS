//Reference: https://bitbucket.org/oosibot1/asp.netproject-businesslogic
//This is not a part of LMS. It is used for role management using WSAT.
//------------------------------------------------------------------------------
// <copyright file="SecUtility.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
 * SecurityUtil class
 * 
 * Copyright (c) 1999 Microsoft Corporation
 */

using System;
using System.Globalization;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Configuration.Provider;

namespace LMS.MembershipController
{
    internal static class SecUtility {
        internal static string GetDefaultAppName() {
            try {
                string appName = System.Web.HttpRuntime.AppDomainAppVirtualPath;
                if (appName == null || appName.Length == 0) {
                    return "/";
                }
                else {
                    return appName;
                }
            }
            catch {
                return "/";
            }
        }

        internal static bool ValidateParameter(ref string param, int maxSize) {
            if (param == null) {
                return false;
            }

            if (param.Trim().Length < 1) {
                return false;
            }

            if (maxSize > 0 && param.Length > maxSize) {
                return false;
            }

            return true;
        }


        internal static bool ValidateParameter(ref string param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize) {
            if (param == null) {
                if (checkForNull) {
                    return false;
                }

                return true;
            }

            param = param.Trim();
            if ((checkIfEmpty && param.Length < 1) ||
                (maxSize > 0 && param.Length > maxSize) ||
                (checkForCommas && param.IndexOf(",") != -1)) {
                    return false;
                }

            return true;
        }

        internal static void CheckParameter(ref string param, int maxSize, string paramName) {
            if (param == null) {
                throw new ArgumentNullException(paramName);
            }

            if (param.Trim().Length < 1) {
                throw new ArgumentException("The parameter '" + paramName + "' must not be empty.",
                                            paramName);
            }

            if (maxSize > 0 && param.Length > maxSize) {
                throw new ArgumentException("The parameter '" + paramName + "' is too long: it must not exceed " + maxSize.ToString(CultureInfo.InvariantCulture) + " chars in length.",
                                            paramName);
            }
        }

        internal static void CheckParameter(ref string param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize, string paramName) {
            if (param == null) {
                if (checkForNull) {
                    throw new ArgumentNullException(paramName);
                }

                return;
            }

            param = param.Trim();

            if (checkIfEmpty && param.Length < 1) {
                throw new ArgumentException("The parameter '" + paramName + "' must not be empty.",
                                            paramName);
            }

            if (maxSize > 0 && param.Length > maxSize) {
                throw new ArgumentException("The parameter '" + paramName + "' is too long: it must not exceed " + maxSize.ToString(CultureInfo.InvariantCulture) + " chars in length.",
                                            paramName);
            }

            if (checkForCommas && param.IndexOf(',') != -1) {
                throw new ArgumentException("The parameter '" + paramName + "' must not contain commas.",
                                            paramName);
            }
        }

        internal static void CheckArrayParameter(ref string[] param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize, string paramName) {
            if (param == null) {
                throw new ArgumentNullException(paramName);
            }

            if (param.Length < 1) {
                throw new ArgumentException("The array parameter '" + paramName + "' should not be empty.", paramName);
            }

            for (int i = param.Length - 1; i >= 0; i--) {
                SecUtility.CheckParameter(ref param[i],
                                          checkForNull,
                                          checkIfEmpty,
                                          checkForCommas,
                                          maxSize,
                                          paramName + "[ " + i.ToString(CultureInfo.InvariantCulture) + " ]");
            }

            for (int i = param.Length - 1; i >= 0; i--) {
                for (int j = i - 1; j >= 0; j--) {
                    if (param[i].Equals(param[j])) {
                        throw new ArgumentException("The array '" + paramName + "' should not contain duplicate values.",
                                                    paramName);
                    }
                }
            }
        }

        internal static bool GetBooleanValue( NameValueCollection config, string valueName, bool defaultValue) {
            string sValue = config[valueName];
            if (sValue == null) {
                return defaultValue;
            }

            if (sValue == "true") {
                return true;
            }

            if (sValue == "false") {
                return false;
            }

            throw new Exception("The value must be a boolean for property '" + valueName + "'");
        }

        internal static int GetIntValue( NameValueCollection config, string valueName, int defaultValue, bool zeroAllowed, int maxValueAllowed) {
            string sValue = config[valueName];

            if (sValue == null) {
                return defaultValue;
            }

            int iValue;
            try {
                iValue = Convert.ToInt32(sValue, CultureInfo.InvariantCulture);
            }
            catch (InvalidCastException e) {
                if (zeroAllowed) {
                    throw new Exception("The value must be a positive integer for property '" + valueName + "'", e);
                }

                throw new Exception("The value must be a positive integer for property '" + valueName + "'", e);
            }

            if (zeroAllowed && iValue < 0) {
                throw new Exception("The value must be a non-negative integer for property '" + valueName + "'");
            }

            if (!zeroAllowed && iValue <= 0) {
                throw new Exception("The value must be a non-negative integer for property '" + valueName + "'");
            }

            if (maxValueAllowed > 0 && iValue > maxValueAllowed) {
                throw new Exception("The value is too big for '" + valueName + "' must be smaller than " + maxValueAllowed.ToString(CultureInfo.InvariantCulture));
            }

            return iValue;
        }

        internal static void CheckSchemaVersion(ProviderBase provider, SqlConnection connection, string[] features, string version, ref int schemaVersionCheck) {
            if (connection == null) {
                throw new ArgumentNullException("connection");
            }

            if (features == null) {
                throw new ArgumentNullException("feature");
            }

            if (version == null) {
                throw new ArgumentNullException("version");
            }

            if (schemaVersionCheck == -1) {
                throw new Exception("The '" + provider.ToString() + "' requires a database schema compatible with schema version '" + version + "'.  However, the current database schema is not compatible with this version.  You may need to either install a compatible schema with aspnet_reqsql.exe (available in the framework installation directory), or upgrade the provider to a newer version.");
            }
            else if (schemaVersionCheck == 0) {
                lock (provider) {
                    if (schemaVersionCheck == -1) {
                        throw new Exception("The '" + provider.ToString() + "' requires a database schema compatible with schema version '" + version + "'.  However, the current database schema is not compatible with this version.  You may need to either install a compatible schema with aspnet_reqsql.exe (available in the framework installation directory), or upgrade the provider to a newer version.");
                    }
                    else if (schemaVersionCheck == 0) {
                        int iStatus = 0;
                        SqlCommand cmd = null;
                        SqlParameter p = null;

                        foreach (string feature in features) {
                            cmd = new SqlCommand("dbo.aspnet_CheckSchemaVersion", connection);

                            cmd.CommandType = CommandType.StoredProcedure;

                            p = new SqlParameter("@Feature", feature);
                            cmd.Parameters.Add(p);

                            p = new SqlParameter("@CompatibleSchemaVersion", version);
                            cmd.Parameters.Add(p);

                            p = new SqlParameter("@ReturnValue", SqlDbType.Int);
                            p.Direction = ParameterDirection.ReturnValue;
                            cmd.Parameters.Add(p);

                            cmd.ExecuteNonQuery();

                            iStatus = ((p.Value != null) ? ((int)p.Value) : -1);
                            if (iStatus != 0) {
                                schemaVersionCheck = -1;

                                throw new Exception("The '" + provider.ToString() + "' requires a database schema compatible with schema version '" + version + "'.  However, the current database schema is not compatible with this version.  You may need to either install a compatible schema with aspnet_reqsql.exe (available in the framework installation directory), or upgrade the provider to a newer version.");
                            }
                        }

                        schemaVersionCheck = 1;
                    }
                }
            }
        }
    }
}