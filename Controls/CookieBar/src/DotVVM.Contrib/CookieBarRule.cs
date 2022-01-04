﻿using System.Collections.Generic;
using System.Reflection;
using System.Text;
using DotVVM.Contrib.Resources;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using Newtonsoft.Json;

namespace DotVVM.Contrib
{
    [JsonConverter(typeof(CookieBarRuleConverter))]
    public class CookieBarRule : DotvvmBindableObject
    {

        [MarkupOptions(AllowBinding = false)]
        public string Key
        {
            get { return (string)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }
        public static readonly DotvvmProperty KeyProperty
            = DotvvmProperty.Register<string, CookieBarRule>(c => c.Key, null);

        [MarkupOptions(AllowBinding = false)]
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DotvvmProperty TitleProperty
            = DotvvmProperty.Register<string, CookieBarRule>(c => c.Title, null);

        [MarkupOptions(AllowBinding = false)]
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
        public static readonly DotvvmProperty DescriptionProperty
            = DotvvmProperty.Register<string, CookieBarRule>(c => c.Description, null);

    }

    public class GoogleAnalyticsRule : CookieBarRule
    {
        public GoogleAnalyticsRule()
        {
            Key = "analytics_storage";
            Title = CookieTexts.RuleTitle_Analytics;
            Description = CookieTexts.Rule_Analytics;
        }
    }

    public class GoogleAdsRule : CookieBarRule
    {
        public GoogleAdsRule()
        {
            Key = "ad_storage";
            Title = CookieTexts.RuleTitle_GoogleAds;
            Description = CookieTexts.Rule_GoogleAds;
        }
    }

    public class FacebookPixelRule : CookieBarRule
    {
        public FacebookPixelRule()
        {
            Key = "fbpixel_storage";
            Title = CookieTexts.RuleTitle_FacebookPixel;
            Description = CookieTexts.Rule_FacebookPixel;
        }
    }

    public class SmartlookRule : CookieBarRule
    {
        public SmartlookRule()
        {
            Key = "smartlook_storage";
            Title = CookieTexts.RuleTitle_Smartlook;
            Description = CookieTexts.Rule_Smartlook;
        }
    }
}
