using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web.Mvc;

namespace QnA_Website.Models
{
    public class RichTextEditorView
    {
        [AllowHtml]
        public string Description
        {
            get;
            set;
        }
    }
}