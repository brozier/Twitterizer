﻿//-----------------------------------------------------------------------
// <copyright file="callback.aspx.cs" company="Patrick 'Ricky' Smith">
//  This file is part of the Twitterizer library (http://code.google.com/p/twitterizer/)
// 
//  Copyright (c) 2010, Patrick "Ricky" Smith (ricky@digitally-born.com)
//  All rights reserved.
//  
//  Redistribution and use in source and binary forms, with or without modification, are 
//  permitted provided that the following conditions are met:
// 
//  - Redistributions of source code must retain the above copyright notice, this list 
//    of conditions and the following disclaimer.
//  - Redistributions in binary form must reproduce the above copyright notice, this list 
//    of conditions and the following disclaimer in the documentation and/or other 
//    materials provided with the distribution.
//  - Neither the name of the Twitterizer nor the names of its contributors may be 
//    used to endorse or promote products derived from this software without specific 
//    prior written permission.
// 
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
//  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
//  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//  IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
//  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
//  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
//  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
//  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
//  POSSIBILITY OF SUCH DAMAGE.
// </copyright>
// <author>Ricky Smith</author>
// <email>ricky@digitally-born.com</email>
// <summary>An example OAuth callback handler.</summary>
//-----------------------------------------------------------------------

using System;
using System.Configuration;
using Twitterizer;
using System.Web.Configuration;

public partial class callback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        OAuthTokenResponse tokens = OAuthUtility.GetAccessToken(
            ConfigurationManager.AppSettings["Twitterizer2.Example.ConsumerKey"],
            ConfigurationManager.AppSettings["Twitterizer2.Example.ConsumerKeySecret"],
            Request.QueryString["oauth_token"]);

        this.AccessTokenLabel.Text = tokens.Token;
        this.AccessTokenSecretLabel.Text = tokens.TokenSecret;
        this.UserIdLabel.Text = tokens.UserId.ToString();
        this.ScreenNameLabel.Text = tokens.ScreenName;

        try
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration(null);
            config.AppSettings.Settings.Add("Twitterizer2.Example.AccessToken", tokens.Token);
            config.AppSettings.Settings.Add("Twitterizer2.Example.AccessTokenSecret", tokens.TokenSecret);
            config.Save();

            ConfigurationManager.RefreshSection("appSettings");

            ResultLabel.Text = @"The web site's configuration has been automatically updated.";
        }
        catch (Exception)
        {
            ResultLabel.Text = @"The web site's configuration could not be updated. Please place these values in the web.config file.";
        }
        
    }
}