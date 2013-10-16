//Reference: https://bitbucket.org/oosibot1/asp.netproject-businesslogic
//This is not a part of LMS. It is used for role management using WSAT.
using System;
using System.Web.Security;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration.Provider;

namespace LMS.MembershipController
{
    /// <summary>
    /// Summary description for LMSMembershipProvider
    /// </summary>
    public class LMSMembershipProvider : MembershipProvider
    {
        private const int SALT_SIZE_IN_BYTES = 16;

        #region Private Field
        private bool _enablePasswordRetrieval;
        private bool _enablePasswordReset;
        private bool _requiresQuestionAndAnswer;
        private string _appName;
        private bool _requiresUniqueEmail;
        private string _databaseFileName;
        private string _hashAlgorithmType;
        private int _applicationId = 0;
        private int _maxInvalidPasswordAttempts;
        private int _passwordAttemptWindow;
        private int _minRequiredPasswordLength;
        private int _minRequiredNonalphanumericCharacters;
        private string _passwordStrengthRegularExpression;
        private DateTime _applicationIdCacheDate;
        private MembershipPasswordFormat _passwordFormat;

        #endregion

        #region Public Properties
        public override bool EnablePasswordRetrieval { get { return _enablePasswordRetrieval; } }

        public override bool EnablePasswordReset { get { return _enablePasswordReset; } }

        public override bool RequiresQuestionAndAnswer { get { return _requiresQuestionAndAnswer; } }

        public override bool RequiresUniqueEmail { get { return _requiresUniqueEmail; } }

        public override MembershipPasswordFormat PasswordFormat { get { return _passwordFormat; } }

        public override int MaxInvalidPasswordAttempts { get { return _maxInvalidPasswordAttempts; } }
        public override int PasswordAttemptWindow { get { return _passwordAttemptWindow; } }

        public override int MinRequiredPasswordLength
        {
            get { return _minRequiredPasswordLength; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _minRequiredNonalphanumericCharacters; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return _passwordStrengthRegularExpression; }
        }

        public override string ApplicationName
        {
            get { return _appName; }
            set
            {
                if (_appName != value)
                {
                    _applicationId = 0;
                    _appName = value;
                }
            }
        }

        #endregion


        #region Public Methods
        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");
            if (String.IsNullOrEmpty(name))
                name = "AccessMembershipProvider";
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Membership $safeprojectname$ Provider");
            }
            base.Initialize(name, config);

            _enablePasswordRetrieval = SecUtility.GetBooleanValue(config, "enablePasswordRetrieval", false);
            _enablePasswordReset = SecUtility.GetBooleanValue(config, "enablePasswordReset", true);
            _requiresQuestionAndAnswer = SecUtility.GetBooleanValue(config, "requiresQuestionAndAnswer", false);
            _requiresUniqueEmail = SecUtility.GetBooleanValue(config, "requiresUniqueEmail", false);
            _maxInvalidPasswordAttempts = SecUtility.GetIntValue(config, "maxInvalidPasswordAttempts", 5, false, 0);
            _passwordAttemptWindow = SecUtility.GetIntValue(config, "passwordAttemptWindow", 10, false, 0);
            _minRequiredPasswordLength = SecUtility.GetIntValue(config, "minRequiredPasswordLength", 7, false, 128);
            _minRequiredNonalphanumericCharacters = SecUtility.GetIntValue(config, "minRequiredNonalphanumericCharacters", 1, true, 128);

            _hashAlgorithmType = config["hashAlgorithmType"];
            if (String.IsNullOrEmpty(_hashAlgorithmType))
            {
                _hashAlgorithmType = "MD5";
            }

            _passwordStrengthRegularExpression = config["passwordStrengthRegularExpression"];
            if (_passwordStrengthRegularExpression != null)
            {
                _passwordStrengthRegularExpression = _passwordStrengthRegularExpression.Trim();
                if (_passwordStrengthRegularExpression.Length != 0)
                {
                    try
                    {
                        Regex regex = new Regex(_passwordStrengthRegularExpression);
                    }
                    catch (ArgumentException e)
                    {
                        throw new ProviderException(e.Message, e);
                    }
                }
            }
            else
            {
                _passwordStrengthRegularExpression = string.Empty;
            }

            _appName = config["applicationName"];
            if (string.IsNullOrEmpty(_appName))
                _appName = SecUtility.GetDefaultAppName();

            if (_appName.Length > 255)
            {
                throw new ProviderException("Provider application name is too long, max length is 255.");
            }

            string strTemp = config["passwordFormat"];
            if (strTemp == null)
                strTemp = "Hashed";

            switch (strTemp)
            {
                case "Clear":
                    _passwordFormat = MembershipPasswordFormat.Clear;
                    break;
                case "Encrypted":
                    _passwordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Hashed":
                    _passwordFormat = MembershipPasswordFormat.Hashed;
                    break;
                default:
                    throw new ProviderException("Bad password format");
            }

            if (_passwordFormat == MembershipPasswordFormat.Hashed && _enablePasswordRetrieval)
                throw new ProviderException("Provider cannot retrieve hashed password");

            _databaseFileName = config["connectionStringName"];
            if (_databaseFileName == null || _databaseFileName.Length < 1)
                throw new ProviderException("Connection name not specified");

            string temp = MyConnectionHelper.GetFileNameFromConnectionName(_databaseFileName, true);
            if (temp == null || temp.Length < 1)
                throw new ProviderException("Connection string not found: " + _databaseFileName);
            _databaseFileName = temp;

            // Make sure connection is good
            MyConnectionHelper.CheckConnectionString(_databaseFileName);

            config.Remove("connectionStringName");
            config.Remove("enablePasswordRetrieval");
            config.Remove("enablePasswordReset");
            config.Remove("requiresQuestionAndAnswer");
            config.Remove("applicationName");
            config.Remove("requiresUniqueEmail");
            config.Remove("maxInvalidPasswordAttempts");
            config.Remove("passwordAttemptWindow");
            config.Remove("passwordFormat");
            config.Remove("name");
            config.Remove("description");
            config.Remove("minRequiredPasswordLength");
            config.Remove("minRequiredNonalphanumericCharacters");
            config.Remove("passwordStrengthRegularExpression");
            config.Remove("hashAlgorithmType");
            if (config.Count > 0)
            {
                string attribUnrecognized = config.GetKey(0);
                if (!String.IsNullOrEmpty(attribUnrecognized))
                    throw new ProviderException("Provider unrecognized attribute: " + attribUnrecognized);
            }
        }


        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            if (!SecUtility.ValidateParameter(ref password,
                                              true,
                                              true,
                                              false,
                                              0))
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            string salt = GenerateSalt();
            string pass = EncodePassword(password, (int)_passwordFormat, salt);
            if (pass.Length > 128)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (!SecUtility.ValidateParameter(ref username,
                                              true,
                                              true,
                                              true,
                                              255))
            {
                status = MembershipCreateStatus.InvalidUserName;
                return null;
            }


            AccessConnectionHolder holder = MyConnectionHelper.GetConnection(_databaseFileName, true);
            SqlConnection connection = holder.Connection;

            try
            {
                try
                {
                    //
                    // Start transaction
                    //

                    SqlCommand command = new SqlCommand();

                    int appId = GetAppplicationId(holder);
                    object result;
                    int uid;

                    ////////////////////////////////////////////////////////////
                    // Step 1: Check if the user exists in the Users table: create if not
                    uid = MyConnectionHelper.GetUserID(connection, appId, username, false);
                    if (uid != 0)
                    { // User not created successfully!
                        status = MembershipCreateStatus.DuplicateUserName;
                        return null;
                    }

                    ////////////////////////////////////////////////////////////
                    // Step 4: Create user in Membership table
                    DateTime dt = MyConnectionHelper.RoundToSeconds(DateTime.Now);
                    command = new SqlCommand(@"INSERT INTO users " +
                                             "(UserName,PasswordHash, Salt) " +
                                             "VALUES (@UserName,@PasswordHash, @salt)",
                                             connection);
                    int pFormat = (int)_passwordFormat;
                    command.Parameters.Add(new SqlParameter("@UserName", username));
                    command.Parameters.Add(new SqlParameter("@PasswordHash", pass));
                    command.Parameters.Add(new SqlParameter("@salt", salt));
                    //
                    // Error inserting row
                    //

                    if (command.ExecuteNonQuery() != 1)
                    {
                        status = MembershipCreateStatus.ProviderError;
                        return null;
                    }

                    status = MembershipCreateStatus.Success;
                    return new MembershipUser(this.Name,
                                              username,
                                              uid,
                                              email,
                                              passwordQuestion,
                                              null,
                                              isApproved,
                                              false,
                                              dt,
                                              dt,
                                              dt,
                                              dt,
                                              DateTime.MinValue);
                }
                catch (Exception e)
                {
                    throw MyConnectionHelper.GetBetterException(e, holder);
                }
                finally
                {
                    holder.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            SecUtility.CheckParameter(ref username, true, true, true, 255, "username");

            AccessConnectionHolder holder = MyConnectionHelper.GetConnection(_databaseFileName, true);
            SqlConnection connection = holder.Connection;
            bool fBeginTransCalled = false;

            try
            {
                try
                {
                    int appId = GetAppplicationId(holder);
                    int userId = MyConnectionHelper.GetUserID(connection, appId, username, false);

                    if (userId == 0)
                        return false; // User not found
                    SqlCommand command;

                    //
                    // Start transaction
                    //

                    command = new SqlCommand("BEGIN TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    fBeginTransCalled = true;

                    bool returnValue = false;
                    if (deleteAllRelatedData)
                    {
                        command = new SqlCommand(@"DELETE FROM UsersInRoles WHERE UserId = @UserId", connection);
                        command.Parameters.Add(new SqlParameter("@UserId", userId));
                        command.ExecuteNonQuery();

                        command = new SqlCommand(@"DELETE FROM Users WHERE UserId = @UserId", connection);
                        command.Parameters.Add(new SqlParameter("@UserId", userId));
                        returnValue = (command.ExecuteNonQuery() == 1);
                    }

                    //
                    // End transaction
                    //

                    command = new SqlCommand("COMMIT TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    fBeginTransCalled = false;

                    return returnValue;
                }
                catch (Exception e)
                {
                    throw MyConnectionHelper.GetBetterException(e, holder);
                }
                finally
                {
                    if (fBeginTransCalled)
                    {
                        try
                        {
                            SqlCommand cmd = new SqlCommand("ROLLBACK TRANSACTION",
                                                            connection);
                            cmd.ExecuteNonQuery();
                        }
                        catch { }
                    }

                    holder.Close();
                }
            }
            catch
            {
                throw;
            }
        }


        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        //private const string s_connPrefix = "Driver={SQL Native Client};Server=.\\SQLExpress; Database=UsersDb1;Trusted_Connection=Yes; AttachDbFilename=|DataDirectory|";
        private const string s_connPrefix = "Driver={SQL Native Client};Server=.\\SQLExpress; Database=UsersDb1;Trusted_Connection=Yes; AttachDbFilename=|DataDirectory|";
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            if (pageIndex < 0)
                throw new ArgumentException("PageIndex cannot be negative");
            if (pageSize < 1)
                throw new ArgumentException("PageSize must be positive");

            long lBound = (long)pageIndex * pageSize;
            long uBound = lBound + pageSize - 1;

            if (uBound > System.Int32.MaxValue)
            {
                throw new ArgumentException("PageIndex too big");
            }

            AccessConnectionHolder holder = MyConnectionHelper.GetConnection(_databaseFileName, true);
            SqlConnection connection = holder.Connection;
            SqlDataReader reader = null;
            long recordCount = 0;
            try
            {
                try
                {
                    int appId = GetAppplicationId(holder);
                    SqlCommand command;
                    MembershipUserCollection users = new MembershipUserCollection();

                    command = new SqlCommand(@"SELECT UserName,UserID  from Users ORDER BY UserName", connection);
                    //command.Parameters.Add(new SqlParameter("@AppId", appId));

                    reader = command.ExecuteReader(CommandBehavior.SequentialAccess);

                    while (reader.Read())
                    {
                        recordCount++;
                        if (recordCount - 1 < lBound || recordCount - 1 > uBound)
                            continue;
                        string username, email, passwordQuestion, comment;
                        DateTime dtCreate, dtLastLogin, dtLastActivity, dtLastPassChange;
                        bool isApproved;
                        int userId;
                        username = GetNullableString(reader, 0);
                        email = "";//GetNullableString(reader, 1);
                        passwordQuestion = ""; //GetNullableString(reader, 2);
                        comment = ""; //GetNullableString(reader, 3);
                        dtCreate = DateTime.Now; //reader.GetDateTime(4);
                        dtLastLogin = DateTime.Now; //reader.GetDateTime(5);
                        dtLastActivity = DateTime.Now; //reader.GetDateTime(6);
                        dtLastPassChange = DateTime.Now; //reader.GetDateTime(7);
                        isApproved = true; //reader.GetBoolean(8);
                        userId = reader.GetInt32(1);
                        users.Add(new MembershipUser("LMSMembershipProvider",
                                                     username,
                                                     userId,
                                                     email,
                                                     passwordQuestion,
                                                     comment,
                                                     isApproved,
                                                     false,
                                                     dtCreate,
                                                     dtLastLogin,
                                                     dtLastActivity,
                                                     dtLastPassChange,
                                                     DateTime.MinValue));
                    }
                    totalRecords = (int)recordCount;
                    return users;
                }
                catch (Exception e)
                {
                    throw new Exception("Exception in creating users Collection", e);
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                    holder.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Exception on line 490: ", e);
            }
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string GetPassword(string username, string answer)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool UnlockUser(string userName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool ValidateUser(string username, string password)
        {
            if (!SecUtility.ValidateParameter(ref username,
                                              true,
                                              true,
                                              false,
                                              255))
            {
                return false;
            }

            if (!SecUtility.ValidateParameter(ref password,
                                              true,
                                              true,
                                              false,
                                              128))
            {
                return false;
            }

            AccessConnectionHolder holder = MyConnectionHelper.GetConnection(_databaseFileName, true);
            SqlConnection connection = holder.Connection;

            try
            {
                try
                {
                    int appId = GetAppplicationId(holder);
                    int userId = MyConnectionHelper.GetUserID(connection, appId, username, false);
                    if (CheckPassword(connection, userId, password))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    throw MyConnectionHelper.GetBetterException(e, holder);
                }
                finally
                {
                    holder.Close();
                }
            }
            catch
            {
                throw;
            }

        }


        private string GenerateSalt()
        {
            byte[] buf = new byte[SALT_SIZE_IN_BYTES];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

        private string EncodePassword(string pass, int passwordFormat, string salt)
        {
            if (passwordFormat == 0) // MembershipPasswordFormat.Clear
                return pass;

            byte[] bIn = Encoding.Unicode.GetBytes(pass);
            byte[] bSalt = Convert.FromBase64String(salt);
            byte[] bAll = new byte[bSalt.Length + bIn.Length];
            byte[] bRet = null;

            Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
            Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);
            if (passwordFormat == 1)
            { // MembershipPasswordFormat.Hashed
                HashAlgorithm s = HashAlgorithm.Create(_hashAlgorithmType);

                // If the hash algorithm is null (and came from config), throw a config exception
                if (s == null)
                {
                    throw new ProviderException("Could not create a hash algorithm");
                }
                bRet = s.ComputeHash(bAll);
            }
            else
            {
                bRet = EncryptPassword(bAll);
            }

            return Convert.ToBase64String(bRet);
        }

        private string UnEncodePassword(string pass, int passwordFormat)
        {
            switch (passwordFormat)
            {
                case 0: // MembershipPasswordFormat.Clear:
                    return pass;
                case 1: // MembershipPasswordFormat.Hashed:
                    throw new ProviderException("Provider can not decode hashed password");
                default:
                    byte[] bIn = Convert.FromBase64String(pass);
                    byte[] bRet = DecryptPassword(bIn);
                    if (bRet == null)
                        return null;
                    return Encoding.Unicode.GetString(bRet, SALT_SIZE_IN_BYTES, bRet.Length - SALT_SIZE_IN_BYTES);
            }
        }

        private string GetPasswordWithFormat(
            SqlConnection connection,
            int userId,
            out int passwordFormat,
            out string passwordSalt)
        {
            SqlCommand command;
            SqlDataReader reader;
            string pass;

            passwordFormat = 1;
            passwordSalt = String.Empty;
            if (userId == 0)
                return null;

            command = new SqlCommand(@"SELECT [PasswordHash],[Salt]" +
                                     @"FROM Users " +
                                     @"WHERE UserId = @UserId",
                                     connection);
            command.Parameters.Add(new SqlParameter("@UserId", userId));

            reader = command.ExecuteReader(CommandBehavior.SingleRow);

            if (!reader.Read())
            { // Zero rows read = user-not-found
                reader.Close();
                return null;
            }

            pass = GetNullableString(reader, 0);
            passwordSalt = GetNullableString(reader, 1);

            switch (_passwordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    passwordFormat = 0;
                    break;
                case MembershipPasswordFormat.Encrypted:
                    passwordFormat = 2;
                    break;
                case MembershipPasswordFormat.Hashed:
                    passwordFormat = 1;
                    break;
                default:
                    throw new ProviderException("Bad password format");
            }

            reader.Close();
            return pass;
        }


        private int GetAppplicationId(AccessConnectionHolder holder)
        {
            if (_applicationId != 0 && holder.CreateDate < _applicationIdCacheDate) // Already cached?
                return _applicationId;
            string appName = _appName;
            if (appName.Length > 255)
                appName = appName.Substring(0, 255);
            _applicationId = 10011;
            _applicationIdCacheDate = DateTime.Now;
            if (_applicationId != 0)
                return _applicationId;
            throw new ProviderException("sorry exception in GetApplicationId");
        }

        private SqlParameter CreateDateTimeSqlParameter(string parameterName, DateTime dt)
        {
            SqlParameter p = new SqlParameter(parameterName, SqlDbType.Timestamp);
            p.Direction = ParameterDirection.Input;
            p.Value = MyConnectionHelper.RoundToSeconds(dt);
            return p;
        }

        ///////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////
        private SqlParameter CreateSqlParam(string paramName, Enum SqlDbType, object objValue)
        {

            SqlParameter param = new SqlParameter(paramName, SqlDbType);

            if (objValue == null)
            {
                param.IsNullable = true;
                param.Value = DBNull.Value;
            }
            else
            {
                param.Value = objValue;
            }

            return param;
        }

        private bool CheckPassword(SqlConnection connection, int userId, string password)
        {
            string salt;
            int passwordFormat, status;
            string pass = GetPasswordWithFormat(connection, userId, out passwordFormat, out salt);
            string pass2 = EncodePassword(password, passwordFormat, salt);
            return (pass == pass2);
        }

        /////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////

        private bool IsStatusDueToBadPassword(int status)
        {
            return (status >= 2 && status <= 6);
        }
        private const int PASSWORD_SIZE = 14;
        public virtual string GeneratePassword()
        {
            return Membership.GeneratePassword(
                MinRequiredPasswordLength < PASSWORD_SIZE ? PASSWORD_SIZE : MinRequiredPasswordLength,
                MinRequiredNonAlphanumericCharacters);
        }
        private string GetNullableString(SqlDataReader reader, int col)
        {
            if (reader.IsDBNull(col) == false)
            {
                return reader.GetString(col);
            }

            return null;
        }
        /////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////
        private string GetExceptionText(int status)
        {
            string key;
            switch (status)
            {
                case 0:
                    return String.Empty;
                case 1:
                    key = "User not found";
                    break;
                case 2:
                    key = "Wrong password";
                    break;
                case 3:
                    key = "Wrong answer";
                    break;
                case 4:
                    key = "Invalid password";
                    break;
                case 5:
                    key = "Invalid question";
                    break;
                case 6:
                    key = "Invalid answer";
                    break;
                case 7:
                    key = "Invalid email";
                    break;
                default:
                    key = "Unknown provider error";
                    break;
            }
            return key;
        }

        #endregion
    }
}
