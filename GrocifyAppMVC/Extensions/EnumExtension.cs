using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

public static class EnumExtension
{
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()?
                        .GetMember(enumValue.ToString())?
                        .First()?
                        .GetCustomAttribute<DisplayAttribute>()?
                        .Name;
    }

	public static MvcHtmlString EnumDisplayNameFor(this HtmlHelper html, Enum status)
    {
        var type = status.GetType();
        var member = type.GetMember(status.ToString());
        DisplayAttribute displayname = (DisplayAttribute)member[0].GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();

        if (displayname != null)
        {
            return new MvcHtmlString(displayname.Name);
        }

        return new MvcHtmlString(status.ToString());
    }
}