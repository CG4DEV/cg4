using System;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ProjectName.WebApp.Attributes
{
    public class KebabCaseNamingAttribute : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            controller.ControllerName = ToKebabCase(controller.ControllerName);

            foreach (var action in controller.Actions)
            {
                action.ActionName = ToKebabCase(action.ActionName);
            }
        }

        private static string ToKebabCase(string input)
        {
            var sb = new StringBuilder(input.Length * 2);

            foreach (var ch in input)
            {
                if (char.IsUpper(ch) && sb.Length > 0)
                {
                    sb.Append('-');
                }

                sb.Append(char.ToLower(ch));
            }

            return sb.ToString();
        }
    }
}