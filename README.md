
![](https://i.imgur.com/NOoCpIg.gif)

# CPPComment Visual Studio Extension

CPPComment is a Visual Studio Extension that generates comments based on [Microsoft XML Docs](https://docs.microsoft.com/en-us/dotnet/csharp/codedoc), inspired by the [CPPTripleSlash](https://github.com/tcbhat/cpptripleslash) Extension. 

I noticed that [CPPTripleSlash](https://github.com/tcbhat/cpptripleslash) and its alternative [CppXmlComment](https://github.com/acmore/CppXmlComments) both do not provide auto detection for parameters and class names, which I personally would like to include within my comments.

Therefor, to differentiate from these other two extensions, it does the following:

  - Detects Class Names
  - Detects if function is a Constructor
  - Detects parameters and their types
  - Detects return type 
  - Detects return type is a pointer
  - Generates comment based on this information

# Usage
1. Move cursor one line above the function you want to comment
2. Type `///`
3. Voila, your comment has been generated :)

### Installation

Download the .vsix file from the [release page](https://github.com/EldinZenderink/CPPComment/releases) or download it from the Visual Studio Marketplace, double click on it, and install. 

### Development

This extension has been developed using Visual Studio 2017. I needed this for my other projects, so I didn't pay much attention to code standards and conventions, basically this is my first extension so it has alot of trial and error code within it and I will not guarrantee that it will always work.  I might change that in the future, if requested, or when I feel like it. 

### Todos

 - Write Tests?
 - Add comments (ironic, isn't it?)
 - Make it readable.

License
----

MIT

(But please do reference me).
**Free Software, Hell Yeah!**
