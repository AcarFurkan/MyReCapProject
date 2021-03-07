using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");//invocation.Method.ReflectedType.FullName name space atarti classsin adini verir. /// invocation.Method.Name// buda calistirdigin methodun ismini verir.
            var arguments = invocation.Arguments.ToList();// methodun parametrelerini listeye cevir.
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";// iki soru isareti varsa soladakinii yoksa sagdakni ekle demek oluyor. //1. de yani parametre null degilse ve toStringe cevrilebiliyorsa.// argument.select parametre degerilini listeye ceverir string join ise virgulle onlari yan yana getirir.
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);// returnvalue yu derse te soyluyordu tam neydi ona bir bak
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
