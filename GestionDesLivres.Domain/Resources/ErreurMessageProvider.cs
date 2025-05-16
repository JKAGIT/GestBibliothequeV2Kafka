// GestionDesLivres.Domain/Resources/ErrorMessageProvider.cs
namespace GestionDesLivres.Domain.Resources
{
    using System.Resources;
    using System.Globalization;
    using System.Reflection;

    public static class ErreurMessageProvider
    {
        private static readonly ResourceManager ResourceManager = new ResourceManager(
            "GestionDesLivres.Domain.Resources.ErreurMessage",
            Assembly.GetExecutingAssembly());

        public static string GetMessage(string key)
        {
            return ResourceManager.GetString(key, CultureInfo.CurrentCulture) ?? key;
        }

        public static string GetMessage(string key, params object[] args)
        {
            string message = GetMessage(key);
            return string.Format(message, args);
        }
    }
}