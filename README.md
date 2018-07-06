# Node.cs
A solid base for any of your node based tools in Unity

## Features
- Create Nodes and move them around
- Connect Nodes with Transitions
- Familiar appearance (similar to Unity's Animator)
- Nodes and Transitions are selectable
- Public variables of both selected Nodes and Transitions are displayed in Inspector
- Pan the view
- Select groupes of Nodes
- Context menu for both Nodes and Transitions
- Easily customizable (The source of styles are GUISkins)

## How to use
### Installation
Clone repository into your Unity project's Assets directory:
```
$ git clone https://github.com/magic-dan/node-cs
```
### Using the editor
#### Open the editor window
Choose *Window/Nodes* menu item. This will open the editor window where nodes can be displayed.

<img src="https://user-images.githubusercontent.com/31962621/42377465-1672eb3a-812c-11e8-82bd-11a46ebf9931.PNG">

#### Create new Node
When the mouse is over an empty space in Nodes Editor Window, open context menu by clicking right mouse button and choose *Add Node/Generic*

<img src="https://user-images.githubusercontent.com/31962621/42377655-d2599ab0-812c-11e8-9176-d4b5d31d49d5.PNG">

You can modify the context menu of Nodes Window by modifying its `DrawContextMenu()` method.

<img src="https://user-images.githubusercontent.com/31962621/42377876-d6514892-812d-11e8-84be-f0306d9a8dd0.PNG">

Context menus of Nodes and Transitions can be modified the same way, by modifying `DrawMenu()` method in corresponding classes.

<img src="https://user-images.githubusercontent.com/31962621/42377939-0c5c4554-812e-11e8-908c-ba9e9368737e.PNG">

#### Connect Nodes
Hover the mouse over a Node from which the Transition should go and open it's context menu by clicking the right mouse button. You should see a Transition, represented by a line with an arrow in a middle of it. While you see the line, click on any Node, except from the one that initialized the Transition. That's it. You have successfully connected the nodes. Congratulations!

<img src="https://user-images.githubusercontent.com/31962621/42377803-8ded9768-812d-11e8-8a43-adc26f9a55f6.PNG">

### Shortcuts
- Delete - delete selected items
- ctrl+s - Save (by default, this method is empty, so feel free to define the saving method yourself)
- ctrl+z - reverse to last saved version (also empty by default)
