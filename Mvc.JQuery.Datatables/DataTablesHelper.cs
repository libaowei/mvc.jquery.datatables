﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Mvc.JQuery.DataTables;
using Mvc.JQuery.DataTables.Models;
using Mvc.JQuery.DataTables.Reflection;

namespace Mvc.JQuery.DataTables
{
    public static class DataTablesHelper
    {
        public static DataTableConfigVm DataTableVm<TController, TResult>(this HtmlHelper html, string id,
            Expression<Func<TController, DataTablesResult<TResult>>> exp, IEnumerable<ColDef> columns = null)
        {
            if (columns == null || !columns.Any())
            {
                columns = typeof(TResult).ColDefs();
            }

            var mi = exp.MethodInfo();
            var controllerName = typeof (TController).Name;
            controllerName = controllerName.Substring(0, controllerName.LastIndexOf("Controller"));
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var ajaxUrl = urlHelper.Action(mi.Name, controllerName);
            return new DataTableConfigVm(id, ajaxUrl, columns);
        }

        public static DataTableConfigVm DataTableVm(this HtmlHelper html, Type t, string id, string uri)
        {
            return new DataTableConfigVm(id, uri.ToString(), t.ColDefs());
        }
        public static DataTableConfigVm DataTableVm<T>(string id, string uri)
        {
            return new DataTableConfigVm(id, uri.ToString(), typeof(T).ColDefs());
        }


        public static DataTableConfigVm DataTableVm<TResult>(this HtmlHelper html, string id, string uri)
        {
            return DataTableVm(html, typeof (TResult), id, uri);
        }

        public static DataTableConfigVm DataTableVm(this HtmlHelper html, string id, string ajaxUrl, params string[] columns)
        {
            return new DataTableConfigVm(id, ajaxUrl, columns.Select(c => new ColDef(c, typeof(string))
            {

            }));
        }

      
    }
}