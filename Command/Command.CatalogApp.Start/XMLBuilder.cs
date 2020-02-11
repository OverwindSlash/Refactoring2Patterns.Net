namespace Command.CatalogApp.Start
{
    public class XmlBuilder
    {
        private TagNode _root;

        public XmlBuilder(string name)
        {
            _root = new TagNode(name);
        }

        public TagNode AddBelowParent(string name)
        {
            TagNode childNode = new TagNode(name);
            _root.AddChild(childNode);
            return childNode;
        }

        public override string ToString()
        {
            return _root.ToXmlString();
        }
    }
}