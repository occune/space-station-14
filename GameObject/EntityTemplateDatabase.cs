﻿using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace GameObject
{
    public class EntityTemplateDatabase
    {
        public Dictionary<string, EntityTemplate> Templates { get; private set; }
        
        public EntityManager EntityManager { get; private set; }

        public EntityTemplateDatabase(EntityManager entityManager)
        {
            EntityManager = entityManager;
            Templates = new Dictionary<string, EntityTemplate>();
            LoadAllTemplates();
        }

        private void LoadAllTemplates()
        {
            string[] templatePaths = Directory.GetFiles(@"EntityTemplates");
            foreach (string path in templatePaths)
                LoadTemplateFromXML(path);
        }

        /// <summary>
        /// Load entity templates from an xml file
        /// </summary>
        /// <param name="path">path to xml file</param>
        public void LoadTemplateFromXML(string path)
        {
            XElement tmp = XDocument.Load(path).Element("EntityTemplates");
            var templates = tmp.Elements("EntityTemplate");
            foreach (XElement e in templates)
            {
                var newTemplate = new EntityTemplate(EntityManager);
                newTemplate.LoadFromXml(e);
                AddTemplate(newTemplate);
            }
        }

        /// <summary>
        /// Add a template directly to the database -- used for creating a template in code instead of xml
        /// </summary>
        /// <param name="template">the template to add</param>
        public void AddTemplate(EntityTemplate template)
        {
            Templates.Add(template.Name, template);
        }

        /// <summary>
        /// Gets a tempate from the db and returns it
        /// </summary>
        /// <param name="templatename"></param>
        /// <returns></returns>
        public EntityTemplate GetTemplate(string templatename)
        {
            if (Templates.ContainsKey(templatename))
                return Templates[templatename];
            return null;
        }
    }
}