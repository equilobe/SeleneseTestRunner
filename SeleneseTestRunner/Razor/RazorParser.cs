using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorEngine.Templating;
using RazorEngine;
using RazorEngine.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace SeleneseTestRunner.Razor
{
    public class RazorParser
    {
        IRazorEngineService engine;

        public string Parse(string rootView, object model)
        {
            var config = new TemplateServiceConfiguration();
            config.DisableTempFileLocking = true;
            config.CachingProvider = new DefaultCachingProvider(s => { });

            using (engine = RazorEngineService.Create(config))
            {
                LoadTemplates();

                var result = engine.Run(rootView, null, model);
                return result;
            }
        }

        private void LoadTemplates()
        {
            var paths = Directory.GetFiles(@".", "*.cshtml", SearchOption.AllDirectories);

            foreach (var path in paths)
                LoadTemplate(path);
        }

        private void LoadTemplate(string path)
        {
            var template = File.ReadAllText(path);
            template = Regex.Replace(template, @"^\@inherits.*[\r\n]+", "", RegexOptions.Multiline);
            var key = Path.GetFileNameWithoutExtension(path);
            engine.AddTemplate(key, template);
            engine.Compile(key);
        }
    }
}
