# Gimbal Task

Google Doc Version of this README:
https://docs.google.com/document/d/1BfCOYtP85jEY19XnY2wmbgGzpKgPAhrvZc5Tv-cqNJs/edit?usp=sharing

## Prompt:


##### [https://playbookxr.notion.site/Unity-Developer-Interview-7d52f6574cb74de99ad8f88e7552d8f3](https://playbookxr.notion.site/Unity-Developer-Interview-7d52f6574cb74de99ad8f88e7552d8f3)


## Implementation Roadmap: (what I planned to do)



1. Set up the project
    1. Create project and add to Github repository
    2. Create README.md
    3. Import mesh and architecture assets
    4. Configure gimbal control prefab
    5. Add basic meshes for gimbal control
    6. Create script files
2. Implement ‘shoddy’ first draft
    7. Add translation and scaling
    8. Add rotation without gimbal locking
    9. Add mouse interactions
    10. Add first draft code annotation
3. Solve gimbal locking issues
    11. Experiment with solutions
        1. Check Youtube videos and Google Search for existing solutions
        2. 4th axis rotation
        3. Euler calculations
        4. Converting world/local spaces
    12. Ensure parent/child relationships are working
    13. Test on pointed arrow mesh (for visibility of direction)
4. Other technical issue solutions
    14. Scaling of gimbal control (so they don’t overlap with mesh)
5. Wrap up for delivery
    15. Final Q/A pass
    16. Second draft of code annotation
    17. Test on an exported build (I think I will only be able to test on Windows for now)
    18. Write build instructions document
    19. Second draft of README.md


## Assets used:

~~[https://assetstore.unity.com/packages/tools/utilities/scriptableobject-architecture-131520](https://assetstore.unity.com/packages/tools/utilities/scriptableobject-architecture-131520)~~

[https://assetstore.unity.com/packages/tools/particles-effects/shapes-173167](https://assetstore.unity.com/packages/tools/particles-effects/shapes-173167)


## Videos referenced:

[https://www.youtube.com/watch?v=z3dDsz4f20A&ab_channel=SkittyAnimates](https://www.youtube.com/watch?v=z3dDsz4f20A&ab_channel=SkittyAnimates)

[https://www.youtube.com/watch?v=kB7iE8Udq5g&ab_channel=WojciechSterna](https://www.youtube.com/watch?v=kB7iE8Udq5g&ab_channel=WojciechSterna)


## Hours spent: _4 hours 10 minutes_

30 minutes creating mock up document

1 hour of research \
10 minutes of prompting ChatGPT \
2 hours spent scripting based on GPT generated code

30 minutes wrapping up for delivery


## Actual (Road)map: (what I did)



1. Set up the project
    1. Create project and add to Github repository
    2. Create README.md
    3. Import mesh and~~ architecture assets~~
        1. _As it turns out I did not need to use the ScriptableObject architecture package as it would be overkill for this task_
    4. Configure gimbal control prefab
    5. Add basic meshes for gimbal control
    6. Create script files
2. Implement ‘shoddy’ first draft
    7. Prompted ChatGPT for basic code
        2. _The code it generated was a solid outline, but didn’t actually solve the problem. I thought this was a fun experiment using ChatGPT to save me some time._
    8. Add translation and scaling
    9. Add rotation without gimbal locking
    10. Add mouse interactions
    11. Add first draft code annotation
3. ~~Solve gimbal locking issues~~
    12. _Using Unity’s RotateAround function there were no Gimbal Locking issues_
4. ~~Other technical issue solutions~~
    13. ~~Scaling of gimbal control (so they don’t overlap with mesh)~~
        3. _Skipped as I feel I am behind the schedule I wanted to be on._
        4. _Will likely go back later to fix these issues as I think it would be fun!_
5. Wrap up for delivery
    14. Final Q/A pass
    15. Second draft of code annotation
    16. Test on an exported build (tested on Windows)
    17. Write build instructions
    18. Second draft of README.md


## Features to add in:



1. Gimbal control scaling fix
    1. Currently the gimbal controls will scale with the object
2. Decoupling Gimbal Control from the specific object so that any object in the scene can be selected and manipulated
3. Adding planar transformation
4. Adding step controls using “Ctrl” key
5. Adding multi-axis gray gimbal controls
6. Adding camera controls


## Build Instructions

Existing build can be found in the zip archive at this Google Drive link:

[https://drive.google.com/file/d/1x5rEWqyZxMfdC0gkF3OfmYF5G83PSRx-/view?usp=sharing](https://drive.google.com/file/d/1x5rEWqyZxMfdC0gkF3OfmYF5G83PSRx-/view?usp=sharing)


### To make a new build 



1. Go to File > Build Settings (Ctrl + Shift + B) to open the Build Settings dialog
2. Ensure base.scene is included in “Scenes in Build”, if not click “Add Open Scenes:
3. Click the ‘Build’ button in the bottom right corner
4. Select a new folder to export the build
