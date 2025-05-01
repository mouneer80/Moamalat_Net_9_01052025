using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Moamalat
{
    public class HttpsValidation : HttpClientHandler
    {


        //Call GenerateSSLpubklickey callback method and repalce here   
        public HttpsValidation()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli;
            UseProxy = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
           
            ServerCertificateCustomValidationCallback = OnValidateCertificate;
        }


        static bool OnValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            var certPublicString = certificate?.GetPublicKeyString();
            var keysMatch = false; 

            if (certificate.Issuer == "CN=DigiCert Global G2 TLS RSA SHA256 2020 CA1, O=DigiCert Inc, C=US" &&
                certificate.Subject == "CN=*.mof.gov.om, O=Ministry of Finance, L=Muscat, C=OM")
                keysMatch = true;

            //if (!keysMatch)
            //{
            //    App.Current.MainPage.DisplayAlert("Not Secured connection", "Sorry,You are trying to access MALIYA app throught a none secured connection", "Exit");
            //}
            return keysMatch;
        }
        static string GenerateSSLPublicKey(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            string certPublicString = certificate?.GetPublicKeyString();
            return certPublicString;
        }
    }
}

