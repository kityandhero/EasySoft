﻿// Global using directives

global using System.ComponentModel;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;
global using EasySoft.Core.AuthenticationCore.Attributes;
global using EasySoft.Core.AuthenticationCore.ExtensionMethods;
global using EasySoft.Core.AuthenticationCore.Filters;
global using EasySoft.Core.AuthenticationCore.Operators;
global using EasySoft.Core.AuthenticationCore.Tools;
global using EasySoft.Core.AutoFac.IocAssists;
global using EasySoft.Core.CacheCore.interfaces;
global using EasySoft.Core.Config.ConfigAssist;
global using EasySoft.Core.DynamicConfig.Assists;
global using EasySoft.Core.ErrorLogTransmitter.Producers;
global using EasySoft.UtilityTools.Core.Assists;
global using EasySoft.UtilityTools.Standard;
global using EasySoft.UtilityTools.Standard.Enums;
global using EasySoft.UtilityTools.Standard.Extensions;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc.Controllers;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.IdentityModel.Tokens;