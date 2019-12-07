Puzzle#
=======
Puzzle# is a simple library for comparing the visual likeness of images. The algorithm used and library implementation
is partly based on fragments from projects such as [image-match](https://github.com/EdjoLabs/image-match) for Python and
[libpuzzle](https://github.com/jedisct1/libpuzzle) for C.

Conceptually, the library accepts an arbitrary image and produces a perception hash of the visual contents, which can 
then be compared to other signatures to determine how closely matched the two images are. It should be noted that this
algorithm is not one that detects conceptually similar visual works; rather, it is suited for comparing edited, 
recoloured, watermarked, or damaged images - detecting copyright violations, for example, would be an appropriate use
case.

## Building
```bash
git clone git@github.com:Nihlus/Puzzle.git
cd Puzzle
dotnet build
``` 

## Installing
Get it on [NuGet]() or compile from source.

## 
