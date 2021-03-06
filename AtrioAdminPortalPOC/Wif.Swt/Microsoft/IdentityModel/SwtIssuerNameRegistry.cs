﻿// <copyright file="SwtIssuerNameRegistry.cs" company="open-source" >
//  Original (c) http://zamd.net/2011/02/08/using-simple-web-token-swt-with-wif/
//  Copyright (adapted version by kzu) NetFx (c) 2011 
//  Copyright binary (c) 2011  by Johnny Halife, Juan Pablo Garcia, Mauro Krikorian, Mariano Converti,
//                                Damian Martinez, Nico Bello, and Ezequiel Morito
//   
//  Redistribution and use in source and binary forms, with or without modification, are permitted.
//
//  The names of its contributors may not be used to endorse or promote products derived from this software without specific prior written permission.
//
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// </copyright>

namespace Microsoft.IdentityModel.Swt
{
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Xml;
    
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// Custom registry that resolves the issuer name for a <see cref="SimpleWebToken"/> 
    /// to one of the configured trusted issuers. If no trusted issuer is specified, 
    /// the token will not be accepted.
    /// </summary>
    public class SwtIssuerNameRegistry : ConfigurationBasedIssuerNameRegistry
    {
        public SwtIssuerNameRegistry()
        {
        }

        public SwtIssuerNameRegistry(XmlNodeList configuration)
            : base(configuration)
        {
        }

        public override string GetIssuerName(SecurityToken securityToken)
        {
            var swt = securityToken as SimpleWebToken;
            if (swt == null)
                return base.GetIssuerName(securityToken);

            // Find a trusted issuer with the same "name" attribute 
            // as the token, if any.
            return this.ConfiguredTrustedIssuers
                .Where(pair => pair.Value == swt.Issuer)
                .Select(pair => pair.Value)
                .FirstOrDefault();
        }
    }
}