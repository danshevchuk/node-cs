#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Nodes
{
    /// <summary>
    /// The base class for all the type of nodes in the Behaviour model editor.
    /// </summary>
    [Serializable]
    public class Node : ScriptableObject
    {
        /// <summary>
        /// Check wheather the node already exists.
        /// </summary>
        public bool Exist { get; protected set; }
        
        /// <summary>
        /// Unique ID of the node.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Unique window ID for the editor.
        /// </summary>
        public int EditorId { get; private set; }
        
        /// <summary>
        /// Name of the node. Reflects the node type.
        /// </summary>
        public string NodeName { get; set; }
        
        /// <summary>
        /// The Rect component of the node.
        /// </summary>
        public Rect Rect { get; set; }
        
        /// <summary>
        /// List of node's transitions.
        /// </summary>
        public List<Transition> Transitions { get { return _transitions;} }
        
        /// <summary>
        /// Wheather the node is currently grouped with another nodes.
        /// </summary>
        public bool Grouped { get; set; }
        
        /// List of node's transitions.
        [HideInInspector][SerializeField] private List<Transition> _transitions = new List<Transition>();
        
        /// <summary>
        /// Empty constructor.
        /// </summary>
        public Node(){}
        
        /// <summary>
        /// Initialize the node.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="nodeName"></param>
        public Node(Rect rect, string nodeName = "Node")
        {
            NodeName = nodeName;
            Rect = rect;
            Exist = true;
        }

        /// <summary>
        /// This function is called when the object is loaded.
        /// </summary>
        public virtual void OnEnable()
        {
            hideFlags = HideFlags.DontSave;
            Exist = true;
        }

        /// <summary>
        /// Update the node's functionality.
        /// </summary>
        public virtual void Update()
        {
            if (Transitions.Count <= 0) return;
            foreach (var transition in Transitions) transition.Draw();
        }

        /// <summary>
        /// Draw the context menu of the node.
        /// </summary>
        public virtual void DrawMenu()
        {
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Transition"), false, StartTransition, null);
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Delete"), false, Delete, null);
            menu.ShowAsContext();
        }
        
        /// <summary>
        /// Initialize the transition starting from this node.
        /// </summary>
        /// <param name="obj"></param>
        private void StartTransition(object obj){
            NodesWindow.StartTransition(Rect, this, NodesWindow.NeutralColor);
        }

        /// <summary>
        /// Node's window component functionality.
        /// </summary>
        /// <param name="id"></param>
        public virtual void NodeFunction(int id)
        {
            EditorId = id;
            if (!Grouped)
            {
                GUI.DragWindow();
            }
            GUILayout.Label("<b>Node</b>");
        }
        
        /// <summary>
        /// Remove the node from the editor.
        /// </summary>
        /// <param name="obj"></param>
        public void Delete(object obj)
        {
            Exist = false;
            NodesWindow.RemoveNode(this);
        }

        /// <summary>
        /// Return the GUI skin of the node.
        /// </summary>
        /// <returns></returns>
        public virtual GUISkin GetSkin()
        {
            return (GUISkin)AssetDatabase.LoadAssetAtPath(
                NodesWindow.PathToStyles + "NodeStyle.guiskin", typeof(GUISkin));
        }

        /// <summary>
        /// Return the default texture of the node.
        /// </summary>
        /// <returns></returns>
        protected virtual Texture2D GetNormalTexture()
        {
            return (Texture2D)AssetDatabase.LoadAssetAtPath(
                NodesWindow.PathToTextures + "rect.png", typeof(Texture2D));
        }

        /// <summary>
        /// Return the 'selected' texture of the node.
        /// </summary>
        /// <returns></returns>
        protected virtual Texture2D GetSelectedTexture()
        {
            return (Texture2D)AssetDatabase.LoadAssetAtPath(
                NodesWindow.PathToTextures + "rectSelected.png", typeof(Texture2D));
        }

        /// <summary>
        /// Return the texture concidering the grouped status of the node.
        /// </summary>
        /// <returns></returns>
        public Texture2D GetTexture()
        {
            return Grouped ? GetSelectedTexture() : GetNormalTexture();
        }
    }
}
#endif