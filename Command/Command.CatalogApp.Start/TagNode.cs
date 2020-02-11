using System.Collections.Generic;
using System.Text;

namespace Command.CatalogApp.Start
{
    public class TagNode
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public StringBuilder Attributes { get; set; }

        public List<TagNode> Children { get; set; }

        public TagNode(string name, string value)
        {
            Name = name;
            Value = value;
            Attributes = new StringBuilder();
            Children = new List<TagNode>();
        }

        public TagNode(string name)
            : this(name, string.Empty)
        {
        }

        public void AddAttribute(string attribute, string value)
        {
            Attributes.Append(" ");
            Attributes.Append(attribute);
            Attributes.Append("='");
            Attributes.Append(value);
            Attributes.Append("'");
        }

        public string ToXmlString()
        {
            string result = string.Empty;
            result += "<" + Name + Attributes.ToString() + ">";
            foreach (var child in Children)
            {
                result += child.ToXmlString();
            }
            result += Value;
            result += "</" + Name + ">";
            return result;
        }

        public void AddChild(TagNode node)
        {
            Children.Add(node);
        }
    }
}
