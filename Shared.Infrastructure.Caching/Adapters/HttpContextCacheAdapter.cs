﻿#region

using System;
using System.Web;
using System.Web.Caching;

#endregion

namespace Shared.Infrastructure.Caching.Adapters
{
    public class HttpContextCacheAdapter : ICacheAdapter
    {
        private readonly Cache _cache;

        public HttpContextCacheAdapter()
        {
            _cache = HttpContext.Current.Cache;
        }

        #region ICacheAdapter Members

        public T GetCachedItem<T>(string key, int duration, Func<T> retrieveFunction) where T : class
        {
            var cachedItem = (T) _cache[key];
            if (cachedItem == null)
            {
                cachedItem = retrieveFunction();
                _cache.Insert(key, cachedItem, null, SystemTime.Now().AddSeconds(duration), TimeSpan.Zero);
            }
            return cachedItem;
        }

        #endregion
    }
}