using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using DotNetOpenAuth.Messaging.Bindings;
using DotNetOpenAuth.OAuth2;
using DotNetOpenAuth.OAuth2.ChannelElements;
using DotNetOpenAuth.OAuth2.Messages;

namespace core.OAuth2
{
    public class OAuth2AuthorizationServer : IAuthorizationServerHost
    {

        //
        // Summary:
        //     Gets the store for storing crypto keys used to symmetrically encrypt and sign
        //     authorization codes and refresh tokens.
        //
        // Remarks:
        //     This store should be kept strictly confidential in the authorization server(s)
        //     and NOT shared with the resource server. Anyone with these secrets can mint tokens
        //     to essentially grant themselves access to anything they want.
        public ICryptoKeyStore CryptoKeyStore { get; }
        //
        // Summary:
        //     Gets the authorization code nonce store to use to ensure that authorization codes
        //     can only be used once.
        public INonceStore NonceStore { get; }

        //
        // Summary:
        //     Determines whether an access token request given a client credential grant should
        //     be authorized and if so records an authorization entry such that subsequent calls
        //     to DotNetOpenAuth.OAuth2.IAuthorizationServerHost.IsAuthorizationValid(DotNetOpenAuth.OAuth2.ChannelElements.IAuthorizationDescription)
        //     would return true.
        //
        // Parameters:
        //   accessRequest:
        //     The access request the credentials came with. This may be useful if the authorization
        //     server wishes to apply some policy based on the client that is making the request.
        //
        // Returns:
        //     A value that describes the result of the authorization check.
        //
        // Exceptions:
        //   T:System.NotSupportedException:
        //     May be thrown if the authorization server does not support the client credential
        //     grant type.
        public AutomatedAuthorizationCheckResponse CheckAuthorizeClientCredentialsGrant(IAccessTokenRequest accessRequest)
        {
            AutomatedAuthorizationCheckResponse response = new AutomatedAuthorizationCheckResponse(accessRequest, true);
            return response;
        }
        //
        // Summary:
        //     Determines whether a given set of resource owner credentials is valid based on
        //     the authorization server's user database and if so records an authorization entry
        //     such that subsequent calls to DotNetOpenAuth.OAuth2.IAuthorizationServerHost.IsAuthorizationValid(DotNetOpenAuth.OAuth2.ChannelElements.IAuthorizationDescription)
        //     would return true.
        //
        // Parameters:
        //   userName:
        //     Username on the account.
        //
        //   password:
        //     The user's password.
        //
        //   accessRequest:
        //     The access request the credentials came with. This may be useful if the authorization
        //     server wishes to apply some policy based on the client that is making the request.
        //
        // Returns:
        //     A value that describes the result of the authorization check.
        //
        // Exceptions:
        //   T:System.NotSupportedException:
        //     May be thrown if the authorization server does not support the resource owner
        //     password credential grant type.
        public AutomatedUserAuthorizationCheckResponse CheckAuthorizeResourceOwnerCredentialGrant(string userName, string password, IAccessTokenRequest accessRequest)
        {
            AutomatedUserAuthorizationCheckResponse response = new AutomatedUserAuthorizationCheckResponse(accessRequest, true, "");
            return response;
        }
        //
        // Summary:
        //     Acquires the access token and related parameters that go into the formulation
        //     of the token endpoint's response to a client.
        //
        // Parameters:
        //   accessTokenRequestMessage:
        //     Details regarding the resources that the access token will grant access to, and
        //     the identity of the client that will receive that access. Based on this information
        //     the receiving resource server can be determined and the lifetime of the access
        //     token can be set based on the sensitivity of the resources.
        //
        // Returns:
        //     A non-null parameters instance that DotNetOpenAuth will dispose after it has
        //     been used.
        public AccessTokenResult CreateAccessToken(IAccessTokenRequest accessTokenRequestMessage)
        {
            AuthorizationServerAccessToken token = new AuthorizationServerAccessToken();
            token.Lifetime = TimeSpan.FromMinutes(2);
            token.ResourceServerEncryptionKey = new RSACryptoServiceProvider();
            token.AccessTokenSigningKey = null;

            AccessTokenResult result = new AccessTokenResult(token);
            return null;
        }
        //
        // Summary:
        //     Gets the client with a given identifier.
        //
        // Parameters:
        //   clientIdentifier:
        //     The client identifier.
        //
        // Returns:
        //     The client registration. Never null.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     Thrown when no client with the given identifier is registered with this authorization
        //     server.
        public IClientDescription GetClient(string clientIdentifier)
        {
            ClientDescription clientDescription = new ClientDescription("", new Uri(""), ClientType.Confidential);
            return clientDescription;
        }
        //
        // Summary:
        //     Determines whether a described authorization is (still) valid.
        //
        // Parameters:
        //   authorization:
        //     The authorization.
        //
        // Returns:
        //     true if the original authorization is still valid; otherwise, false.
        //
        // Remarks:
        //     When establishing that an authorization is still valid, it's very important to
        //     only match on recorded authorizations that meet these criteria:
        //     1) The client identifier matches. 2) The user account matches. 3) The scope on
        //     the recorded authorization must include all scopes in the given authorization.
        //     4) The date the recorded authorization was issued must be no later that the date
        //     the given authorization was issued.
        //     One possible scenario is where the user authorized a client, later revoked authorization,
        //     and even later reinstated authorization. This subsequent recorded authorization
        //     would not satisfy requirement #4 in the above list. This is important because
        //     the revocation the user went through should invalidate all previously issued
        //     tokens as a matter of security in the event the user was revoking access in order
        //     to sever authorization on a stolen account or piece of hardware in which the
        //     tokens were stored.
        public bool IsAuthorizationValid(IAuthorizationDescription authorization)
        {
            return false;
        }
    }
}
