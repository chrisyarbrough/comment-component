#Comment Component

Add this component to a GameObject to display a readonly text and 
an icon for documentation purposes or similar.

![img](Documentation~/MainExample.png)

Double click or press CTRL + Enter/Return to edit the text and select an icon.

![img](Documentation~/MainExample_Editing.png)

Comments within scenes are removed during the build process and will not add overhead to the player.
There's a preferences option to toggle the logging of removed comments.

##Compatibility

The package was tested with Unity 2020.3.

##Limitations
Comments are not removed from prefabs which are not referenced in a scene (e.g. in the Resources folder).
