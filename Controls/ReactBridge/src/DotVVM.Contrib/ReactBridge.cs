﻿using System;
using System.Collections.Generic;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Utils;
using Newtonsoft.Json;

namespace DotVVM.Contrib
{
    /// <summary>
    /// Renders a react control
    /// </summary>
    public class ReactBridge : HtmlGenericControl
    {
        public ReactBridge() : base("div")
        {
        }

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        [MarkupOptions(Required = true)]
        public static readonly DotvvmProperty NameProperty 
            = DotvvmProperty.Register<string, ReactBridge>(c => c.Name);

        [MarkupOptions(MappingMode = MappingMode.Attribute, AllowBinding = true, AllowHardCodedValue = true)]
        [PropertyGroup(new[] { "update:" })]
        public Dictionary<string, object> Update { get; private set; } = new Dictionary<string, object>();

        KnockoutBindingGroup CreateProps()
        {
            var props = new KnockoutBindingGroup();
            foreach (var attr in Attributes)
            {
                props.Add(attr.Key,
                    attr.Value is IValueBinding binding ? $"dotvvm.serialization.serialize({binding.GetKnockoutBindingExpression()})" :
                    (attr.Value is IStaticValueBinding staticBinding ? staticBinding.Evaluate(this) : attr.Value).Apply(JsonConvert.SerializeObject)
                );
            }
            foreach (var update in this.Update)
            {
                var knockoutExpression = update.Value as IValueBinding;
                props.Add(update.Key, "function (a) {(" + knockoutExpression.GetKnockoutBindingExpression() + ")(a)}");
            }
            return props;
        }
        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            var binding = new KnockoutBindingGroup();
            binding.Add("component", (string)GetValue(NameProperty));
            binding.Add("props", CreateProps().ToString());
            writer.AddKnockoutDataBind("dotvvm-contrib-ReactBridge", binding);
            Attributes.Clear();
        }
    }
}
