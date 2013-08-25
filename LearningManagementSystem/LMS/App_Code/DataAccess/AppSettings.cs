using System.Configuration;

namespace LMS.DataAccess
{
  /// <summary>
  /// The AppConfiguaration class reads the setting from web.config.
  /// </summary>
  public static class AppSettings
  {

    #region Public Properties

    /// <summary>
    /// Returns the connectionstring  for the application.
    /// </summary>
    public static string ConnectionString
    {
      get
      {
          return ConfigurationManager.ConnectionStrings["LMSServices"].ConnectionString;
      }
    }
    #endregion

  }
}