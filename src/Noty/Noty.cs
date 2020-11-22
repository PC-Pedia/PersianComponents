﻿using Microsoft.AspNetCore.Mvc.Rendering;
using DNZ.MvcComponents;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AspNetCore.Mvc
{
    public class Noty : MessageBoxResult
    {
        //https://ned.im/noty/
        //https://cdnjs.com/libraries/noty
        //https://cdnjs.com/libraries/jquery-noty
        private const string noty_packaged_js_cdn = "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/jquery-noty/2.4.1/packaged/jquery.noty.packaged.min.js\" integrity=\"sha512-deW7s7mlh1kdsULBlS05epcSl1Zze2KafJ4KH5kyOP3MkAYCbVC3lrVYoQ2lM1AlaWR3jYm+Myiad2sluDPoEg==\" crossorigin=\"anonymous\"></script>";
        private const string noty_packaged_min_js = "DNZ.MvcComponents.Noty.jquery.noty.packaged.min.js";
        public Dictionary<string, object> ButtonAttributes { get; set; }

        public Noty(IHtmlHelper helper = null) : base(helper)
        {
            RenderScriptAndStyle.ScriptOnce(ComponentUtility.GetJsTag(noty_packaged_min_js, noty_packaged_js_cdn));
            ButtonAttributes = new Dictionary<string, object>();
        }

        public Noty Text(string value)
        {
            Attributes["text"] = string.Format("'{0}'", value);
            SetScript();
            return this;
        }

        public Noty Type(MessageType value)
        {
            Attributes["type"] = string.Format("'{0}'", value.ToString().ToLower());
            SetScript();
            return this;
        }

        public Noty Layout(MessageAlignment value)
        {
            Attributes["layout"] = string.Format("'{0}'", value.ToString().ToLowerFirst());
            SetScript();
            return this;
        }

        public Noty DismissQueue(bool value)
        {
            Attributes["dismissQueue"] = value.ToString().ToLower();
            SetScript();
            return this;
        }

        public Noty Modal(bool value)
        {
            Attributes["modal"] = value.ToString().ToLower();
            SetScript();
            return this;
        }

        public Noty Timeout(int value)
        {
            Attributes["timeout"] = value;
            SetScript();
            return this;
        }

        public Noty MaxVisible(int value)
        {
            Attributes["maxVisible"] = value;
            SetScript();
            return this;
        }

        public Noty Killer(bool value)
        {
            Attributes["killer"] = value.ToString().ToLower();
            SetScript();
            return this;
        }

        public Noty AddButton(string text, string onClick, string addClass = null)
        {
            var str = @"{
            text: '" + text + @"',
            " + (string.IsNullOrEmpty(addClass) ? "" : "addClass: '" + addClass + "',") + @"
            onClick: " + onClick + @"
        }";
            ButtonAttributes.Add("", str);
            SetScript();
            return this;
        }

        protected void SetScript()
        {
            if (ButtonAttributes.Count > 0)
            {
                Attributes["buttons"] = "[\n" + string.Join(", \n", ButtonAttributes.Select(p => p.Value)) + "\n]";
            }

            Script = "$.noty.closeAll(); noty(" + this.RenderOptions() + ")";

            if (!ComponentUtility.GetHttpContext().Request.IsAjaxRequest() && HtmlHelper == null)
            {
                SetScriptTag();
            }
        }
    }
}