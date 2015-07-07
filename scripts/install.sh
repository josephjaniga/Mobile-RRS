#! /bin/sh
 
# Example install script for Unity3D project. See the entire example: https://github.com/JonathanPorta/ci-build
echo 'Installing Unity.pkg'
sudo installer -dumplog -package Unity.pkg -target /