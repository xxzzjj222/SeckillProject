using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Common.Users
{
    /// <summary>
    /// 自定义模型绑定提供者(提供SysUserModelBinder)
    /// </summary>
    public class SysUserModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentException(nameof(context));
            if(context.Metadata.ModelType==typeof(SysUser))
                return new BinderTypeModelBinder(typeof(SysUserHeaderModelBinder));
            return null;
        }
    }
}
