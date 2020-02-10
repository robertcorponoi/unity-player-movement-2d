<h1 align="center">Unity Player Movement 2D</h1>

<p align="center">A quick and simple way to get started with creating a 2D player character.<p>

This package provides you with an easy way to add player movement to your 2D game.

## **Structure**

The following assets are available as part of the package:

- Scenes
  - **SampleScene**: A demo scene with the Player and Ground prefab that can be used to see how it all works together.

- Art
  - **Characters**: A sprite sheet used to create the demo character.

  - **Terrain**: A sprite sheet used to create the demo ground layer.

- Animations
  - **Animator**: An animator with transitions and parameters already set up for idle, walking/running, and jumping/landing.

  - **Animations**: Demo animations for use with the animator, however it is recommended you replace them with your own custom animations based around your player character.

- Prefabs
  - **Player**: The Player prefab contains all of the essential components and the movement script with everything set up. You can update this component with your own sprite and it will be good to go.

  - **Ground**: Used for demoing how to create ground objects, basically just creating a Ground layer and adding it as a reference to the script.

## **Getting Started**

If you are new to player momvement in Unity, then it is recommended to check out the sample scene and play it so that you can see what is looks/functions like.

Otherwise, you can just check out the Player prefab which has the required components and script and modify the sprite to your own custom one.

You will also need to update the animations used in the animator for walk/run, jump, and jump land. The states, triggers, and parameters should not be changed but the animations in the states should be set to your own custom animations.

## **A Note About Jumping**

Since jumping is a bit more complex than just walking/running we'll go over it in a little more detail.

To start off, jump height is determined by the adjustable property in the inspecter of the script.

In terms of jump animation, it is currently broken up into two parts, jump and jump fall. The jump animation is played until the player has reached the peak of their jump and from there until they hit the ground the jump fall animation is played. This lets you have more control and if you choose you can just omit the animation for jump fall and not use it at all.

## **Properties**

The following properties of the script can be adjusted in the inspector:

### **Components:**

- **Sprite Renderer:** A reference to the Sprite Renderer component of the player.

- **RigidBody:** A reference to the RigidBody2D component of the player.

- **Animator:** A reference to the Animator component of the player.

### **Walking/Running:**

- **Walk Speed:** The speed at which the character should walk at in the horizontal direction.

### **Jumping:**

- **Ground Layer**: A reference to the layer that is applied to all game objects that represent the ground.

- **Jump Height:** The amount of force behind the player's jump. There's not really any metric with this just play around until you find a jump height that works best for you.
