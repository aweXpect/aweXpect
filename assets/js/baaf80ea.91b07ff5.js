"use strict";(self.webpackChunkpages=self.webpackChunkpages||[]).push([[4427],{4982:(e,t,s)=>{s.r(t),s.d(t,{assets:()=>r,contentTitle:()=>o,default:()=>p,frontMatter:()=>c,metadata:()=>n,toc:()=>d});const n=JSON.parse('{"id":"expectations/guid","title":"Guid","description":"Describes the possible expectations for Guid values.","source":"@site/docs/expectations/guid.md","sourceDirName":"expectations","slug":"/expectations/guid","permalink":"/docs/expectations/guid","draft":false,"unlisted":false,"editUrl":"https://github.com/aweXpect/aweXpect/tree/main/Docs/pages/docs/expectations/guid.md","tags":[],"version":"current","sidebarPosition":9,"frontMatter":{"sidebar_position":9},"sidebar":"tutorialSidebar","previous":{"title":"DateOnly / TimeOnly","permalink":"/docs/expectations/date-time-only"},"next":{"title":"Stream","permalink":"/docs/expectations/stream"}}');var a=s(4848),i=s(8453);const c={sidebar_position:9},o="Guid",r={},d=[{value:"Equality",id:"equality",level:2},{value:"Empty",id:"empty",level:2}];function u(e){const t={code:"code",h1:"h1",h2:"h2",header:"header",p:"p",pre:"pre",...(0,i.R)(),...e.components};return(0,a.jsxs)(a.Fragment,{children:[(0,a.jsx)(t.header,{children:(0,a.jsx)(t.h1,{id:"guid",children:"Guid"})}),"\n",(0,a.jsxs)(t.p,{children:["Describes the possible expectations for ",(0,a.jsx)(t.code,{children:"Guid"})," values."]}),"\n",(0,a.jsx)(t.h2,{id:"equality",children:"Equality"}),"\n",(0,a.jsxs)(t.p,{children:["You can verify, that the ",(0,a.jsx)(t.code,{children:"Guid"})," is equal to another one or not:"]}),"\n",(0,a.jsx)(t.pre,{children:(0,a.jsx)(t.code,{className:"language-csharp",children:'Guid subject = Guid.Parse("5c01d9d2-66f7-4782-8c14-e54eae9aaacc");\n\nawait Expect.That(subject).IsEqualTo(Guid.Parse("5c01d9d2-66f7-4782-8c14-e54eae9aaacc"))\n  .Because("they are the same");\nawait Expect.That(subject).IsNotEqualTo(Guid.Parse("cdd7a485-40a1-4bba-bb8b-d0e903704b02"))\n  .Because("they differ");\n'})}),"\n",(0,a.jsx)(t.h2,{id:"empty",children:"Empty"}),"\n",(0,a.jsxs)(t.p,{children:["You can verify, that the ",(0,a.jsx)(t.code,{children:"Guid"})," is empty or not:"]}),"\n",(0,a.jsx)(t.pre,{children:(0,a.jsx)(t.code,{className:"language-csharp",children:"await Expect.That(Guid.Empty).IsEmpty();\nawait Expect.That(Guid.NewGuid()).IsNotEmpty();\n"})})]})}function p(e={}){const{wrapper:t}={...(0,i.R)(),...e.components};return t?(0,a.jsx)(t,{...e,children:(0,a.jsx)(u,{...e})}):u(e)}},8453:(e,t,s)=>{s.d(t,{R:()=>c,x:()=>o});var n=s(6540);const a={},i=n.createContext(a);function c(e){const t=n.useContext(i);return n.useMemo((function(){return"function"==typeof e?e(t):{...t,...e}}),[t,e])}function o(e){let t;return t=e.disableParentContext?"function"==typeof e.components?e.components(a):e.components||a:c(e.components),n.createElement(i.Provider,{value:t},e.children)}}}]);