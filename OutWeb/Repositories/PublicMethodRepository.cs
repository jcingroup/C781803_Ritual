﻿using OutWeb.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Web;

namespace OutWeb.Repositories
{
    public static class PublicMethodRepository
    {
        private static string m_customLanguageCode = string.Empty;

        private static Language m_language = Language.NotSet;


        /// <summary>
        /// 使用者自訂語系
        /// </summary>
        public static string CustomLanguageCode { get { return m_customLanguageCode; } set { m_customLanguageCode = value; } }

        /// <summary>
        /// 取得瀏覽器使用語言清單
        /// </summary>
        public static string[] SupportLanguage
        {
            get
            {
                if (ConfigurationManager.AppSettings["SupportLanguage"] != null)
                {
                    string[] aSupportLang = ConfigurationManager.AppSettings["SupportLanguage"].Split(',');
                    List<string> lstSupportLang = new List<string>();
                    foreach (string langCode in aSupportLang)
                        lstSupportLang.Add(langCode.Trim());
                    return lstSupportLang.ToArray();
                }

                if (ConfigurationManager.AppSettings["DefaultLanguage"] != null)
                    return new string[] { ConfigurationManager.AppSettings["DefaultLanguage"].Trim() };
                return new string[] { "zh-TW" };
            }
        }

        /// <summary>
        /// 取得目前瀏覽器使用語系
        /// </summary>
        public static string CurrentLanguageCode
        {
            get
            {
                string sCurrLang = null;
                if (string.IsNullOrEmpty(m_customLanguageCode))
                {
                    //// 使用瀏覽器語系
                    //sCurrLang = HttpContext.Current.Request.UserLanguages[0].Split(',')[0];

                    // 使用瀏覽器語系
                    if ((String.IsNullOrEmpty(sCurrLang)) && (HttpContext.Current.Request.UserLanguages.Length != 0))
                        sCurrLang = HttpContext.Current.Request.UserLanguages[0].Split(',')[0];

                    // 使用系統語系
                    if (String.IsNullOrEmpty(sCurrLang))
                        sCurrLang = System.Globalization.CultureInfo.CurrentCulture.CompareInfo.Name;

                    // 本系統是否支援目前的語系
                    if (SupportLanguage.Where(g => g.Equals(sCurrLang)).FirstOrDefault() == null)
                        sCurrLang = null;

                    // 不支援目前語系則取預設語系
                    if ((String.IsNullOrEmpty(sCurrLang)) && (ConfigurationManager.AppSettings["DefaultLanguage"] != null))
                        sCurrLang = ConfigurationManager.AppSettings["DefaultLanguage"].Trim();
                    return sCurrLang;

                }
                else
                    sCurrLang = m_customLanguageCode;
                return sCurrLang;
            }
        }

        /// <summary>
        /// 取得目前使用語系列舉
        /// </summary>
        /// <returns></returns>
        public static Language CurrentLanguageEnum
        {
            get
            {
                return m_language;
            }
            set
            {
                m_language = value;
            }
        }

        public static string GetCode(this Language lang)
        {
            return GetEnumDescription<Language>(lang);
        }

        /// <summary>
        ///將DateTime轉換為10碼字串 (ex. 2017-05-05)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ConvertDateTimeTo10CodeString(this DateTime dt)
        {
            return string.Format("{0:yyyy\\-MM\\-dd}", dt);
        }

        /// <summary>
        /// 取得WebConfig的AppSetting
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        #region 取列舉名稱

        /// <summary>
        /// 取得語系的列舉
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Language GetLanguageEnumByCode(string code)
        {
            foreach (var v in Enum.GetValues(typeof(Language)))
            {
                Language en = (Language)v;
                string thisCode = GetEnumDescriptValue<Language>(en);
                if (string.IsNullOrEmpty(thisCode))
                    continue;
                var splitStr = thisCode.Split(new string[] { "$$" }, StringSplitOptions.RemoveEmptyEntries);
                if (splitStr.Length > 1)
                {
                    if (!thisCode.Split(new string[] { "$$" }, StringSplitOptions.RemoveEmptyEntries)[1].Equals(code))
                        continue;
                    else
                    {
                        m_language = en;
                        return en;
                    }
                }
                else
                    if (!thisCode.Split(new string[] { "$$" }, StringSplitOptions.RemoveEmptyEntries)[0].Equals(code))
                    continue;
                else
                {
                    m_language = en;
                    return en;
                }
            }
            throw new Exception("get language enum have some error.");
        }

        /// <summary>
        /// 依照列舉值取得列舉
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="code"></param>
        /// <returns></returns>
        public static TEnum GetEnumByValue<TEnum>(string value)
        {
            foreach (var v in Enum.GetValues(typeof(TEnum)))
            {
                TEnum en = (TEnum)v;
                var enStr = en.ToString();
                if (!enStr.Equals(value))
                    continue;
                else
                    return en;
            }
            throw new Exception("get enum have some error.");
        }

        #endregion 取列舉名稱

        #region 取列舉自訂描述

        /// <summary>
        /// 取列舉自訂描述
        /// </summary>
        /// <typeparam name="Tenum"></typeparam>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetEnumDescription<Tenum>(Tenum en)
        {
            string status = en.GetEnumDescriptValue();
            return status.Split(new string[] { "$$" }, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        private static string GetEnumDescriptValue<TEnum>(this TEnum en)
        {
            DescriptionAttribute[] enumCodeAttrs = GetEnumCustomAttributes<TEnum, DescriptionAttribute>(en);
            if (enumCodeAttrs.Length == 0)
                return string.Empty;
            return enumCodeAttrs[0].Description;
        }

        private static TAttribute[] GetEnumCustomAttributes<TEnum, TAttribute>(TEnum en)
        {
            System.Reflection.MemberInfo[] enumMembers = typeof(TEnum).GetMember(en.ToString());
            if (enumMembers.Length == 0)
                throw new ArgumentException("Type [" + typeof(TEnum).Name + "] is not contents member: " + en.ToString());
            return enumMembers[0].GetCustomAttributes(typeof(TAttribute), false) as TAttribute[];
        }

        #endregion 取列舉自訂描述
    }
}