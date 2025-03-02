"use strict";(self.webpackChunkpages=self.webpackChunkpages||[]).push([[5354],{7056:(e,t,i)=>{i.r(t),i.d(t,{assets:()=>o,contentTitle:()=>r,default:()=>h,frontMatter:()=>l,metadata:()=>s,toc:()=>c});const s=JSON.parse('{"id":"extensions/project/Testably/index","title":"[aweXpect.Testably](https://github.com/aweXpect/aweXpect.Testably) [![Nuget](https://img.shields.io/nuget/v/aweXpect.Testably)](https://www.nuget.org/packages/aweXpect.Testably)","description":"Expectations for the file and time system from Testably.Abstractions.","source":"@site/docs/extensions/project/Testably/00-index.md","sourceDirName":"extensions/project/Testably","slug":"/extensions/project/Testably/index","permalink":"/docs/extensions/project/Testably/index","draft":false,"unlisted":false,"editUrl":"https://github.com/aweXpect/aweXpect/tree/main/Docs/pages/docs/extensions/project/Testably/00-index.md","tags":[],"version":"current","sidebarPosition":0,"frontMatter":{},"sidebar":"extensionsSidebar","previous":{"title":"aweXpect.Web","permalink":"/docs/extensions/project/Web/index"},"next":{"title":"Write your own extension","permalink":"/docs/extensions/write-extensions"}}');var n=i(4848),a=i(8453);const l={},r="aweXpect.Testably Nuget",o={},c=[{value:"File system",id:"file-system",level:2},{value:"File",id:"file",level:3},{value:"Directory",id:"directory",level:3}];function m(e){const t={a:"a",code:"code",h1:"h1",h2:"h2",h3:"h3",header:"header",img:"img",p:"p",pre:"pre",...(0,a.R)(),...e.components};return(0,n.jsxs)(n.Fragment,{children:[(0,n.jsx)(t.header,{children:(0,n.jsxs)(t.h1,{id:"awexpecttestably-nuget",children:[(0,n.jsx)(t.a,{href:"https://github.com/aweXpect/aweXpect.Testably",children:"aweXpect.Testably"})," ",(0,n.jsx)(t.a,{href:"https://www.nuget.org/packages/aweXpect.Testably",children:(0,n.jsx)(t.img,{src:"https://img.shields.io/nuget/v/aweXpect.Testably",alt:"Nuget"})})]})}),"\n",(0,n.jsxs)(t.p,{children:["Expectations for the file and time system from ",(0,n.jsx)(t.a,{href:"https://github.com/Testably/Testably.Abstractions",children:"Testably.Abstractions"}),"."]}),"\n",(0,n.jsx)(t.h2,{id:"file-system",children:"File system"}),"\n",(0,n.jsx)(t.p,{children:"You can verify that a specific file or directory exists in the file system:"}),"\n",(0,n.jsx)(t.pre,{children:(0,n.jsx)(t.code,{className:"language-csharp",children:'IFileSystem fileSystem = new MockFileSystem();\nfileSystem.Directory.CreateDirectory("my/path");\nfileSystem.File.WriteAllText("my-file.txt", "some content");\n\nawait That(fileSystem).HasDirectory("my/path");\nawait That(fileSystem).HasFile("my-file.txt");\n'})}),"\n",(0,n.jsx)(t.h3,{id:"file",children:"File"}),"\n",(0,n.jsx)(t.p,{children:"For files, you can verify the file content:"}),"\n",(0,n.jsx)(t.pre,{children:(0,n.jsx)(t.code,{className:"language-csharp",children:'IFileSystem fileSystem = new MockFileSystem();\nfileSystem.File.WriteAllText("my-file.txt", "some content");\n\nawait That(fileSystem).HasFile("my-file.txt").WithContent("some content").IgnoringCase();\nawait That(fileSystem).HasFile("my-file.txt").WithContent().NotEqualTo("some unexpected content");\n'})}),"\n",(0,n.jsx)(t.p,{children:"You can also verify the file content with regard to another file:"}),"\n",(0,n.jsx)(t.pre,{children:(0,n.jsx)(t.code,{className:"language-csharp",children:'IFileSystem fileSystem = new MockFileSystem();\nfileSystem.File.WriteAllText("my-file.txt", "some content");\nfileSystem.File.WriteAllText("my-other-file.txt", "SOME CONTENT");\nfileSystem.File.WriteAllText("my-third-file.txt", "some other content");\n\nawait That(fileSystem).HasFile("my-file.txt").WithContent().SameAs("my-other-file.txt").IgnoringCase();\nawait That(fileSystem).HasFile("my-file.txt").WithContent().NotSameAs("my-third-file.txt");\n'})}),"\n",(0,n.jsx)(t.p,{children:"For files, you can verify the creation time, last access time and last write time:"}),"\n",(0,n.jsx)(t.pre,{children:(0,n.jsx)(t.code,{className:"language-csharp",children:'IFileSystem fileSystem = new MockFileSystem();\nfileSystem.File.WriteAllText("my-file.txt", "some content");\n\nawait That(sut).HasFile(path).WithCreationTime(DateTime.Now).Within(1.Second());\nawait That(sut).HasFile(path).WithLastAccessTime(DateTime.Now).Within(1.Second());\nawait That(sut).HasFile(path).LastWriteTime(DateTime.Now).Within(1.Second());\n'})}),"\n",(0,n.jsx)(t.h3,{id:"directory",children:"Directory"}),"\n",(0,n.jsx)(t.p,{children:"For directories, you can verify that they contain subdirectories:"}),"\n",(0,n.jsx)(t.pre,{children:(0,n.jsx)(t.code,{className:"language-csharp",children:'IFileSystem fileSystem = new MockFileSystem();\nfileSystem.Directory.CreateDirectory("foo/bar1");\nfileSystem.Directory.CreateDirectory("foo/bar2/baz");\n\nawait That(fileSystem).HasDirectory("foo").WithDirectories(f => f.HasCount().EqualTo(2));\n'})}),"\n",(0,n.jsx)(t.p,{children:"For directories, you can verify that they contain files:"}),"\n",(0,n.jsx)(t.pre,{children:(0,n.jsx)(t.code,{className:"language-csharp",children:'IFileSystem fileSystem = new MockFileSystem();\nfileSystem.Directory.CreateDirectory("foo/bar");\nfileSystem.File.WriteAllText("foo/bar/my-file.txt", "some content");\n\nawait That(fileSystem).HasDirectory("foo/bar").WithFiles(f => f.All().ComplyWith(x => x.HasContent("SOME CONTENT").IgnoringCase()));\n'})})]})}function h(e={}){const{wrapper:t}={...(0,a.R)(),...e.components};return t?(0,n.jsx)(t,{...e,children:(0,n.jsx)(m,{...e})}):m(e)}},8453:(e,t,i)=>{i.d(t,{R:()=>l,x:()=>r});var s=i(6540);const n={},a=s.createContext(n);function l(e){const t=s.useContext(a);return s.useMemo((function(){return"function"==typeof e?e(t):{...t,...e}}),[t,e])}function r(e){let t;return t=e.disableParentContext?"function"==typeof e.components?e.components(n):e.components||n:l(e.components),s.createElement(a.Provider,{value:t},e.children)}}}]);