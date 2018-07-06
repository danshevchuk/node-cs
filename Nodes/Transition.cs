#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Nodes
{
	/// <summary>
	/// Represents the Transition Component of a Behaviour model.
	/// </summary>
	public class Transition : ScriptableObject
	{
		/// <summary>
		/// The node from which the transition is started.
		/// </summary>
		public Node Start
		{
			get { return _start; }
			set { _start = value; }
		}
		
		/// <summary>
		/// The node to which the transition is pointing.
		/// </summary>
		public Node End
		{
			get { return _end; }
			set { _end = value; }
		}
		
		/// <summary>
		/// Color of the transiiton graphics.
		/// </summary>
		public Color Color { get; set; }

		/// <summary>
		/// Wheather the transition is currently selected.
		/// </summary>
		public bool IsSelected { private get; set; }

		/// The node from which the transition is started.
		[HideInInspector][SerializeField] private Node _start;
		/// The node to which the transition is pointing.
		[HideInInspector][SerializeField] private Node _end;

		/// <summary>
		/// Initialize the transition.
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="color"></param>
		public Transition(Node start, Node end, Color? color = null)
		{
			Start = start;
			End = end;
			Color = color ?? NodesWindow.NeutralColor;
		}

		/// <summary>
		/// This function is called when object is loaded.
		/// </summary>
		public void OnEnable()
		{
			hideFlags = HideFlags.DontSave;
		}

		/// <summary>
		/// Render the graphics.
		/// </summary>
		public void Draw()
		{
			if (!End.Exist)
			{
				Start.Transitions.Remove(this);
				return;
			}

			var startPos = new Vector3(Start.Rect.x + Start.Rect.width / 2,
				Start.Rect.y + Start.Rect.height / 2, 0);
			var endPos = new Vector3(End.Rect.x + End.Rect.width / 2, End.Rect.y + End.Rect.height / 2, 0);
			Handles.color = IsSelected ? NodesWindow.SelectedColor : Color;
			Handles.DrawAAPolyLine(4, startPos, endPos);
			DrawArrow();
		}
		
		/// <summary>
		/// Draw the context menu of the transition.
		/// </summary>
		public void DrawMenu()
		{
			var menu = new GenericMenu();
			menu.AddItem(new GUIContent("Delete"), false, Delete, null);
			menu.ShowAsContext();
		}

		/// <summary>
		/// Check wheather specific point on the screen is inside the boundaries
		/// of the transition's graphics.
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public bool Contains(Vector2 point)
		{
			return Math.Abs(Vector2.Distance(Start.Rect.center, point) + Vector2.Distance(End.Rect.center, point) - Vector2.Distance(Start.Rect.center, End.Rect.center)) < 0.2f;
		}
		
		/// <summary>
		/// Render the arrow on top of the transition.
		/// </summary>
		private void DrawArrow()
		{
			var arrowHead = new Vector3[3];
			
			var forward = (Vector3)(End.Rect.center - Start.Rect.center).normalized;
			var right = Vector3.Cross(Vector3.forward, forward).normalized;
			var size = HandleUtility.GetHandleSize(End.Rect.center);
			var width = size * 0.3f;
			var height = size * 0.5f;

			var len = (End.Rect.center - Start.Rect.center).magnitude;
			Vector3 cen = Start.Rect.center + (len*0.5f+height/2f) * (Vector2)forward;
			arrowHead[0] = cen;
			arrowHead[1] = cen - forward * height + right*width;
			arrowHead[2] = cen - forward * height - right*width;
			
			Handles.DrawAAConvexPolygon(arrowHead);
		}
		
		/// <summary>
		/// Remove this transition from the editor.
		/// </summary>
		/// <param name="obj"></param>
		public void Delete(object obj)
		{
			Start.Transitions.Remove(this);
		}
	}
}
#endif