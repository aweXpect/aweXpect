"use strict";(self.webpackChunkpages=self.webpackChunkpages||[]).push([[2056],{3301:(e,t,n)=>{n.r(t),n.d(t,{assets:()=>c,contentTitle:()=>o,default:()=>u,frontMatter:()=>r,metadata:()=>s,toc:()=>l});const s=JSON.parse('{"id":"extensibility/write-extensions","title":"Write your own extension","description":"This library will never be able to cope with all ideas and use cases. Therefore, it is possible to use the [","source":"@site/docs/extensibility/write-extensions.md","sourceDirName":"extensibility","slug":"/extensibility/write-extensions","permalink":"/aweXpect/docs/extensibility/write-extensions","draft":false,"unlisted":false,"editUrl":"https://github.com/aweXpect/aweXpect/tree/main/Docs/pages/docs/extensibility/write-extensions.md","tags":[],"version":"current","sidebarPosition":1,"frontMatter":{"sidebar_position":1},"sidebar":"tutorialSidebar","previous":{"title":"Extensibility","permalink":"/aweXpect/docs/category/extensibility"},"next":{"title":"Customization","permalink":"/aweXpect/docs/extensibility/customization"}}');var i=n(4848),a=n(8453);const r={sidebar_position:1},o="Write your own extension",c={},l=[{value:"Example",id:"example",level:2},{value:"Constraints",id:"constraints",level:2}];function d(e){const t={a:"a",br:"br",code:"code",em:"em",h1:"h1",h2:"h2",header:"header",li:"li",p:"p",pre:"pre",ul:"ul",...(0,a.R)(),...e.components};return(0,i.jsxs)(i.Fragment,{children:[(0,i.jsx)(t.header,{children:(0,i.jsx)(t.h1,{id:"write-your-own-extension",children:"Write your own extension"})}),"\n",(0,i.jsxs)(t.p,{children:["This library will never be able to cope with all ideas and use cases. Therefore, it is possible to use the ",(0,i.jsxs)(t.a,{href:"https://www.nuget.org/packages/aweXpect.Core/",children:["\n",(0,i.jsx)(t.code,{children:"aweXpect.Core"})]})," package and write your own extensions.\nGoal of this package is to be more stable than the main aweXpect package, so reduce the risk of version conflicts\nbetween different extensions."]}),"\n",(0,i.jsxs)(t.p,{children:["You can extend the functionality for any types, by adding extension methods on ",(0,i.jsx)(t.code,{children:"IThat<TType>"}),"."]}),"\n",(0,i.jsx)(t.h2,{id:"example",children:"Example"}),"\n",(0,i.jsx)(t.p,{children:"You want to verify that a string corresponds to an absolute path."}),"\n",(0,i.jsx)(t.pre,{children:(0,i.jsx)(t.code,{className:"language-csharp",children:'/// <summary>\n///     Verifies that the <paramref name="subject"/> is an absolute path.\n/// </summary>\npublic static AndOrResult<string, IThat<string>> IsAbsolutePath(\n  this IThat<string> subject)\n  => new(subject.ExpectationBuilder.AddConstraint(it\n      => new IsAbsolutePathConstraint(it)),\n    subject);\n\nprivate readonly struct IsAbsolutePathConstraint(string it) : IValueConstraint<string>\n{\n  public ConstraintResult IsMetBy(string actual)\n  {\n    var absolutePath = Path.GetFullPath(actual);\n    if (absolutePath == actual)\n    {\n      return new ConstraintResult.Success<string>(actual, "be an absolute path");\n    }\n\n    return new ConstraintResult.Failure("be an absolute path",\n      $"{it} found {Formatter.Format(actual)}");\n  }\n}\n'})}),"\n",(0,i.jsx)(t.h2,{id:"constraints",children:"Constraints"}),"\n",(0,i.jsxs)(t.p,{children:["The basis for expectations are constraints. You can add different constraints to the ",(0,i.jsx)(t.code,{children:"ExpectationBuilder"})," that is\navailable for the ",(0,i.jsx)(t.code,{children:"IThat<T>"}),". They differ in the input and output parameters:"]}),"\n",(0,i.jsxs)(t.ul,{children:["\n",(0,i.jsxs)(t.li,{children:[(0,i.jsx)(t.code,{children:"IValueConstraint<T>"}),(0,i.jsx)(t.br,{}),"\n","It receives the actual value ",(0,i.jsx)(t.code,{children:"T"})," and returns a ",(0,i.jsx)(t.code,{children:"ConstraintResult"}),"."]}),"\n",(0,i.jsxs)(t.li,{children:[(0,i.jsx)(t.code,{children:"IAsyncConstraint<T>"}),(0,i.jsx)(t.br,{}),"\n","It receives the actual value ",(0,i.jsx)(t.code,{children:"T"})," and a ",(0,i.jsx)(t.code,{children:"CancellationToken"})," and returns the ",(0,i.jsx)(t.code,{children:"ConstraintResult"})," asynchronously.",(0,i.jsx)(t.br,{}),"\n",(0,i.jsxs)(t.em,{children:["Use it when you need asynchronous functionality or access to the timeout ",(0,i.jsx)(t.code,{children:"CancellationToken"}),"."]})]}),"\n",(0,i.jsxs)(t.li,{children:[(0,i.jsx)(t.code,{children:"IContextConstraint<T>"})," / ",(0,i.jsx)(t.code,{children:"IAsyncContextConstraint<T>"}),(0,i.jsx)(t.br,{}),"\n","Similar to the ",(0,i.jsx)(t.code,{children:"IValueConstraint<T>"})," and ",(0,i.jsx)(t.code,{children:"IAsyncConstraint<T>"})," respectively but receives an additional\n",(0,i.jsx)(t.code,{children:"IEvaluationContext"})," parameter that allows storing and receiving data between expectations.",(0,i.jsx)(t.br,{}),"\n",(0,i.jsxs)(t.em,{children:["This mechanism is used for example to avoid enumerating an ",(0,i.jsx)(t.code,{children:"IEnumerable"})," multiple times across multiple constraints."]})]}),"\n"]})]})}function u(e={}){const{wrapper:t}={...(0,a.R)(),...e.components};return t?(0,i.jsx)(t,{...e,children:(0,i.jsx)(d,{...e})}):d(e)}},8453:(e,t,n)=>{n.d(t,{R:()=>r,x:()=>o});var s=n(6540);const i={},a=s.createContext(i);function r(e){const t=s.useContext(a);return s.useMemo((function(){return"function"==typeof e?e(t):{...t,...e}}),[t,e])}function o(e){let t;return t=e.disableParentContext?"function"==typeof e.components?e.components(i):e.components||i:r(e.components),s.createElement(a.Provider,{value:t},e.children)}}}]);