# Mlp-Magic-for-Unity
I created a script for Unity that allows you to levitate objects using MLP-like magic. For this project, I used "mlp-magic" made by RoBorg (Link: https://github.com/RoBorg/mlp-magic ). In the demo, I used Standard Assets from Unity.

![image](https://user-images.githubusercontent.com/52779411/204134799-a9113efd-beed-4ef4-a17a-bc41810cba0f.png)


Initially, I developed this script for my personal use in any MLP projects where it might be required. However, I realized that there's no better time than the present to start sharing my work on GitHub. Hence, I created this repository.
How this stuff works: at the start, you need to add MaskCamera as a child of your main camera, then add CameraEffect to your main camera and grab your MaskCamera in the Mask Camera field. Then, you need to add a magic script to your player.
If you want the player to be able to lift an object, simply add the tag "Levitate."
Controls Q is for magic, and the mouse wheel is for a change in the distance to the controlled object.
