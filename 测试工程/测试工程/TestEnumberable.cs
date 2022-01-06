using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 测试工程
{


 

    public class LinkNode
    {
        public LinkNode(string nodeName)
        {
            NodeName = nodeName;
        }
        public string NodeName;
        public void LoadWork() {
            Console.WriteLine(NodeName);
        }
    }
    
    public class LinkNodeList: IEnumerable
    {
        private List<LinkNode> liskNodes = new List<LinkNode>();

        public void Add(LinkNode linkNode)
        {
            liskNodes.Add(linkNode);
        }
        public IEnumerator<LinkNode> GetEnumerator()
        {
            foreach (var item in liskNodes)
            {
                yield return item;
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in liskNodes)
            {
                yield return item;
            }
            yield break;
        }
    }

    public class LinkNodeEnumerator : IEnumerator
    {
        private List<LinkNode> linkNodes = new List<LinkNode>();

        private int currentIndex = -1;

        public LinkNodeEnumerator(List<LinkNode> linkNodes)
        {
            this.linkNodes = linkNodes;
        }

        public object Current {
            get 
            {
                if (currentIndex > linkNodes.Count || currentIndex < 0)
                {
                    return null;
                }
                else
                {
                    return linkNodes[currentIndex];
                }
            }
        }

        public bool MoveNext()
        {
            currentIndex++;
            if (currentIndex < linkNodes.Count && linkNodes[currentIndex] != null)
            {
                return true;
            }
            return false;
        }

        public void Reset()
        {
            currentIndex = 0;
        }
    }
}
