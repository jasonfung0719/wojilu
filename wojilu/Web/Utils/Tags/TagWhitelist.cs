﻿/*
 * Copyright 2010 www.wojilu.com
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Text;
using wojilu.Web.Mvc;

namespace wojilu.Web.Utils.Tags {

    internal class TagWhitelist {

        private static readonly Dictionary<String, String> whitelist = getConfigWhiteList();

        private static Dictionary<String, String> getConfigWhiteList() {

            Dictionary<String, String> defaultValues = getDefaultAllowedTags();

            if (MvcConfig.Instance.TagWhitelist.Count == 0) return defaultValues;

            Dictionary<String, String> dic = new Dictionary<String, String>();
            foreach (String rTag in MvcConfig.Instance.TagWhitelist) {

                if (strUtil.IsNullOrEmpty( rTag )) continue;

                String key = rTag.ToLower();

                if (defaultValues.ContainsKey( key ) == false) continue;

                dic.Add( key, defaultValues[key] );

            }

            return dic;
        }

        // 默认允许属性id,class,style
        private static Dictionary<String, String> getDefaultAllowedTags() {

            Dictionary<String, String> dic = new Dictionary<String, String>();

            dic.Add( "a", "href,target,title" );
            dic.Add( "img", "src,alt,title,border,width,height" );
            dic.Add( "hr", "" );
            dic.Add( "ol", "" );
            dic.Add( "ul", "" );
            dic.Add( "li", "" );

            dic.Add( "p", "align" );
            dic.Add( "br", "" );
            dic.Add( "div", "" );
            dic.Add( "span", "" );
            dic.Add( "table", "cellpadding,cellspacing,width,border" );
            dic.Add( "tbody", "" );
            dic.Add( "tr", "" );
            dic.Add( "th", "" );
            dic.Add( "td", "width,valign,align" );

            dic.Add( "object", "data,type,width,height,classid,codebase,align" );
            dic.Add( "param", "name,value" );
            dic.Add( "embed", "src,bgcolor,name,align,quality,pluginspage,type,width,height,allowFullScreen,allowScriptAccess" );

            dic.Add( "blockquote", "" );
            dic.Add( "pre", "" );

            dic.Add( "font", "size,color,face" );
            dic.Add( "b", "" );
            dic.Add( "strong", "" );
            dic.Add( "i", "" );
            dic.Add( "u", "" );
            dic.Add( "s", "" );
            dic.Add( "em", "" );
            dic.Add( "strike", "" );
            dic.Add( "sup", "" );
            dic.Add( "sub", "" );
            dic.Add( "style", "" );

            return dic;
        }



        public static Dictionary<String, String> GetInstance() {

            return whitelist;


        }

        public static Dictionary<String, String> AppendTags( String tags ) {

            Dictionary<String, String> _cfgWhiteList = GetInstance();

            if (strUtil.IsNullOrEmpty( tags )) return _cfgWhiteList;

            String[] arrTags = tags.ToLower().Split( new char[] { ',', '/', '|' } );

            Dictionary<String, String> dic = new Dictionary<string, string>();
            foreach (KeyValuePair<String, String> kv in _cfgWhiteList) {
                dic.Add( kv.Key, kv.Value );
            }

            foreach (String tag in arrTags) {

                if (dic.ContainsKey( tag )) continue;
                dic.Add( tag, "" );
            }

            return dic;

        }

        public static Dictionary<String, String> GetTags( String tags ) {

            if (strUtil.IsNullOrEmpty( tags )) return new Dictionary<String, String>();

            Dictionary<String, String> _cfgWhiteList = GetInstance();

            String[] arrTags = tags.ToLower().Split( new char[] { ',', '/', '|' } );

            Dictionary<String, String> dic = new Dictionary<string, string>();

            foreach (String tag in arrTags) {

                if (dic.ContainsKey( tag )) continue;

                foreach (KeyValuePair<String, String> kv in _cfgWhiteList) {
                    if (tag.Equals( kv.Key )) {
                        dic.Add( kv.Key, kv.Value );
                    }
                }
            }

            return dic;
        }

    }
}
