"use strict";(self.webpackChunkpages=self.webpackChunkpages||[]).push([[4425],{9074:(e,n,s)=>{s.r(n),s.d(n,{assets:()=>r,contentTitle:()=>c,default:()=>u,frontMatter:()=>i,metadata:()=>t,toc:()=>l});const t=JSON.parse('{"id":"expectations/enum","title":"Enum","description":"Describes the possible expectations for enum values.","source":"@site/docs/expectations/enum.md","sourceDirName":"expectations","slug":"/expectations/enum","permalink":"/docs/expectations/enum","draft":false,"unlisted":false,"editUrl":"https://github.com/aweXpect/aweXpect/tree/main/Docs/pages/docs/expectations/enum.md","tags":[],"version":"current","sidebarPosition":5,"frontMatter":{"sidebar_position":5},"sidebar":"tutorialSidebar","previous":{"title":"Number","permalink":"/docs/expectations/number"},"next":{"title":"TimeSpan","permalink":"/docs/expectations/timespan"}}');var a=s(4848),o=s(8453);const i={sidebar_position:5},c="Enum",r={},l=[{value:"Equality",id:"equality",level:2},{value:"Value",id:"value",level:2},{value:"Defined",id:"defined",level:2},{value:"Flags",id:"flags",level:2}];function d(e){const n={code:"code",h1:"h1",h2:"h2",header:"header",p:"p",pre:"pre",...(0,o.R)(),...e.components};return(0,a.jsxs)(a.Fragment,{children:[(0,a.jsx)(n.header,{children:(0,a.jsx)(n.h1,{id:"enum",children:"Enum"})}),"\n",(0,a.jsxs)(n.p,{children:["Describes the possible expectations for ",(0,a.jsx)(n.code,{children:"enum"})," values."]}),"\n",(0,a.jsx)(n.h2,{id:"equality",children:"Equality"}),"\n",(0,a.jsxs)(n.p,{children:["You can verify, that the ",(0,a.jsx)(n.code,{children:"enum"})," is equal to another one or not:"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:"enum Colors { Red = 1, Green = 2, Blue = 3}\n\nawait Expect.That(Colors.Red).IsEqualTo(Colors.Red)\n  .Because(\"it is 'Red'\");\nawait Expect.That(Colors.Red).IsNotEqualTo(Colors.Blue)\n  .Because(\"it is 'Red'\");\n"})}),"\n",(0,a.jsx)(n.h2,{id:"value",children:"Value"}),"\n",(0,a.jsxs)(n.p,{children:["You can verify, that the ",(0,a.jsx)(n.code,{children:"enum"})," has a given value or not:"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:"enum Colors { Red = 1, Green = 2, Blue = 3}\n\nawait Expect.That(Colors.Red).HasValue(1)\n  .Because(\"'Red' is 1\");\nawait Expect.That(Colors.Red).DoesNotHaveValue(2)\n  .Because(\"'Red' is 1\");\n"})}),"\n",(0,a.jsx)(n.h2,{id:"defined",children:"Defined"}),"\n",(0,a.jsxs)(n.p,{children:["You can verify, that the ",(0,a.jsx)(n.code,{children:"enum"})," has a defined value or not:"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'enum Colors { Red = 1, Green = 2, Blue = 3}\n\nawait Expect.That((Colors)3).IsDefined()\n  .Because("3 corresponds to \'Blue\'");\nawait Expect.That((Colors)4).IsNotDefined()\n  .Because("4 is no valid color");\n'})}),"\n",(0,a.jsx)(n.h2,{id:"flags",children:"Flags"}),"\n",(0,a.jsxs)(n.p,{children:["You can verify, that the ",(0,a.jsx)(n.code,{children:"enum"})," has a specific flag or not:"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:"RegexOptions subject = RegexOptions.Multiline | RegexOptions.IgnoreCase;\n\nawait Expect.That(subject).HasFlag(RegexOptions.IgnoreCase)\n  .Because(\"it has the 'IgnoreCase' flag\");\nawait Expect.That(subject).DoesNotHaveFlag(RegexOptions.ExplicitCapture)\n  .Because(\"it does not have the 'ExplicitCapture' flag\");\n"})})]})}function u(e={}){const{wrapper:n}={...(0,o.R)(),...e.components};return n?(0,a.jsx)(n,{...e,children:(0,a.jsx)(d,{...e})}):d(e)}},8453:(e,n,s)=>{s.d(n,{R:()=>i,x:()=>c});var t=s(6540);const a={},o=t.createContext(a);function i(e){const n=t.useContext(o);return t.useMemo((function(){return"function"==typeof e?e(n):{...n,...e}}),[n,e])}function c(e){let n;return n=e.disableParentContext?"function"==typeof e.components?e.components(a):e.components||a:i(e.components),t.createElement(o.Provider,{value:n},e.children)}}}]);