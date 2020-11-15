﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wowthing.Web.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        private static readonly TimeSpan CACHE_DURATION = TimeSpan.FromDays(365);

        public static IApplicationBuilder UseStaticFilesWithCaching(this IApplicationBuilder app)
        {
            return app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = (ctx) =>
                {
                    if (ctx.Context.Request.Path.StartsWithSegments("/dist/"))
                    {
                        var headers = ctx.Context.Response.GetTypedHeaders();
                        headers.CacheControl = new CacheControlHeaderValue
                        {
                            Public = true,
                            MaxAge = CACHE_DURATION,
                        };
                    }
                }
            });
        }
    }
}