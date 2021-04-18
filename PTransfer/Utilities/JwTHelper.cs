using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace PTransfer.Utilities {
    public class JwTHelper {
        public static string GenrateJwT(Dictionary<string, string> payload) {
            try {
                RSAParameters rsaParameters;
                string privateKey = Constants.PRIVATE_KEY_JWT;
                using (var tr = new StringReader(privateKey)) {
                    var pemReader = new PemReader(tr);
                    var keyPair = pemReader.ReadObject() as AsymmetricCipherKeyPair;
                    if (keyPair == null) {
                        throw new Exception("Could not read the private key");
                    }
                    var privateRsaParams = keyPair.Private as RsaPrivateCrtKeyParameters;
                    rsaParameters = DotNetUtilities.ToRSAParameters(privateRsaParams);
                    using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider()) {
                        rsa.ImportParameters(rsaParameters);
                        return Jose.JWT.Encode(payload, rsa, Jose.JwsAlgorithm.RS256);
                    }
                }
            } catch (Exception e) {
                Logger.logError(typeof(JwTHelper).Name, e.ToString());
                return null;
            }
        }
        public static string ValidateJwT(string token) {
            try {
                RSAParameters rsaParameters;
                string publicKey = Constants.PUBLIC_KEY_JWT;
                using (var tr = new StringReader(publicKey)) {
                    var pemReader = new PemReader(tr);
                    var publicKeyParams = pemReader.ReadObject() as RsaKeyParameters;
                    if (publicKeyParams == null) {
                        throw new Exception("Could not read the public key");
                    }
                    rsaParameters = DotNetUtilities.ToRSAParameters(publicKeyParams);
                    using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider()) {
                        rsa.ImportParameters(rsaParameters);                        
                        return Jose.JWT.Decode(token, rsa, Jose.JwsAlgorithm.RS256);
                    }
                }
            } catch (Exception e) {
                Logger.logError(typeof(JwTHelper).Name, e.ToString());
                return null;
            }
        }
    }
}
