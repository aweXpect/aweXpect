"use strict";(self.webpackChunkpages=self.webpackChunkpages||[]).push([[369],{1591:(e,t,n)=>{n.r(t),n.d(t,{assets:()=>i,contentTitle:()=>c,default:()=>h,frontMatter:()=>l,metadata:()=>a,toc:()=>r});const a=JSON.parse('{"id":"expectations/boolean","title":"Boolean","description":"Describes the possible expectations for boolean values.","source":"@site/docs/expectations/boolean.md","sourceDirName":"expectations","slug":"/expectations/boolean","permalink":"/aweXpect/docs/expectations/boolean","draft":false,"unlisted":false,"editUrl":"https://github.com/aweXpect/aweXpect/tree/main/Docs/pages/docs/expectations/boolean.md","tags":[],"version":"current","sidebarPosition":2,"frontMatter":{"sidebar_position":2},"sidebar":"tutorialSidebar","previous":{"title":"Object","permalink":"/aweXpect/docs/expectations/object"},"next":{"title":"String","permalink":"/aweXpect/docs/expectations/string"}}');var o=n(4848),s=n(8453);const l={sidebar_position:2},c="Boolean",i={},r=[{value:"Equality",id:"equality",level:2},{value:"True / False",id:"true--false",level:2},{value:"Implication",id:"implication",level:2}];function d(e){const t={a:"a",code:"code",em:"em",h1:"h1",h2:"h2",header:"header",p:"p",pre:"pre",...(0,s.R)(),...e.components};return(0,o.jsxs)(o.Fragment,{children:[(0,o.jsx)(t.header,{children:(0,o.jsx)(t.h1,{id:"boolean",children:"Boolean"})}),"\n",(0,o.jsx)(t.p,{children:"Describes the possible expectations for boolean values."}),"\n",(0,o.jsx)(t.h2,{id:"equality",children:"Equality"}),"\n",(0,o.jsxs)(t.p,{children:["You can verify, that the ",(0,o.jsx)(t.code,{children:"bool"})," is equal to another one or not:"]}),"\n",(0,o.jsx)(t.pre,{children:(0,o.jsx)(t.code,{className:"language-csharp",children:"bool subject = false;\n\nawait Expect.That(subject).Should().Be(false);\nawait Expect.That(subject).Should().NotBe(true);\n"})}),"\n",(0,o.jsx)(t.h2,{id:"true--false",children:"True / False"}),"\n",(0,o.jsxs)(t.p,{children:["You can verify, that the ",(0,o.jsx)(t.code,{children:"bool"})," is ",(0,o.jsx)(t.code,{children:"true"})," or ",(0,o.jsx)(t.code,{children:"false"}),":"]}),"\n",(0,o.jsx)(t.pre,{children:(0,o.jsx)(t.code,{className:"language-csharp",children:"await Expect.That(false).Should().BeFalse();\nawait Expect.That(true).Should().BeTrue();\n"})}),"\n",(0,o.jsx)(t.p,{children:"The negation is only available for nullable booleans:"}),"\n",(0,o.jsx)(t.pre,{children:(0,o.jsx)(t.code,{className:"language-csharp",children:'bool? subject = null;\n\nawait Expect.That(subject).Should().NotBeFalse()\n  .Because("it could be true or null");\nawait Expect.That(subject).Should().NotBeTrue()\n  .Because("it could be false or null");\n'})}),"\n",(0,o.jsx)(t.h2,{id:"implication",children:"Implication"}),"\n",(0,o.jsxs)(t.p,{children:["You can verify, that ",(0,o.jsx)(t.code,{children:"a"})," implies ",(0,o.jsx)(t.code,{children:"b"})," (",(0,o.jsxs)(t.em,{children:["find ",(0,o.jsx)(t.a,{href:"https://mathworld.wolfram.com/Implies.html",children:"here"})," a mathematical explanation"]}),"):"]}),"\n",(0,o.jsx)(t.pre,{children:(0,o.jsx)(t.code,{className:"language-csharp",children:"bool a = false;\nbool b = true;\n\nawait Expect.That(a).Should().Imply(b);\n"})})]})}function h(e={}){const{wrapper:t}={...(0,s.R)(),...e.components};return t?(0,o.jsx)(t,{...e,children:(0,o.jsx)(d,{...e})}):d(e)}},8453:(e,t,n)=>{n.d(t,{R:()=>l,x:()=>c});var a=n(6540);const o={},s=a.createContext(o);function l(e){const t=a.useContext(s);return a.useMemo((function(){return"function"==typeof e?e(t):{...t,...e}}),[t,e])}function c(e){let t;return t=e.disableParentContext?"function"==typeof e.components?e.components(o):e.components||o:l(e.components),a.createElement(s.Provider,{value:t},e.children)}}}]);