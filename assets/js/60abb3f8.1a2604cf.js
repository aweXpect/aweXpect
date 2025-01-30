"use strict";(self.webpackChunkpages=self.webpackChunkpages||[]).push([[8479],{2184:(e,n,t)=>{t.r(n),t.d(n,{assets:()=>o,contentTitle:()=>c,default:()=>d,frontMatter:()=>r,metadata:()=>s,toc:()=>l});const s=JSON.parse('{"id":"expectations/string","title":"String","description":"Describes the possible expectations for strings.","source":"@site/docs/expectations/string.md","sourceDirName":"expectations","slug":"/expectations/string","permalink":"/docs/expectations/string","draft":false,"unlisted":false,"editUrl":"https://github.com/aweXpect/aweXpect/tree/main/Docs/pages/docs/expectations/string.md","tags":[],"version":"current","sidebarPosition":3,"frontMatter":{"sidebar_position":3},"sidebar":"tutorialSidebar","previous":{"title":"Boolean","permalink":"/docs/expectations/boolean"},"next":{"title":"Number","permalink":"/docs/expectations/number"}}');var a=t(4848),i=t(8453);const r={sidebar_position:3},c="String",o={},l=[{value:"Equality",id:"equality",level:2},{value:"Wildcards",id:"wildcards",level:3},{value:"Regular expressions",id:"regular-expressions",level:3},{value:"One of",id:"one-of",level:2},{value:"Null, empty or white-space",id:"null-empty-or-white-space",level:2},{value:"Length",id:"length",level:2},{value:"String start / end",id:"string-start--end",level:2},{value:"Contains",id:"contains",level:2},{value:"Character casing",id:"character-casing",level:2}];function h(e){const n={a:"a",br:"br",code:"code",h1:"h1",h2:"h2",h3:"h3",header:"header",li:"li",p:"p",pre:"pre",table:"table",tbody:"tbody",td:"td",th:"th",thead:"thead",tr:"tr",ul:"ul",...(0,i.R)(),...e.components};return(0,a.jsxs)(a.Fragment,{children:[(0,a.jsx)(n.header,{children:(0,a.jsx)(n.h1,{id:"string",children:"String"})}),"\n",(0,a.jsx)(n.p,{children:"Describes the possible expectations for strings."}),"\n",(0,a.jsx)(n.h2,{id:"equality",children:"Equality"}),"\n",(0,a.jsxs)(n.p,{children:["You can verify, that the ",(0,a.jsx)(n.code,{children:"string"})," is equal to another one.",(0,a.jsx)(n.br,{}),"\n","This expectation can be configured to ignore case, ignore newline style, ignoring leading or trailing white-space, or\nuse a custom ",(0,a.jsx)(n.code,{children:"IEqualityComparer<string>"}),":"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'string subject = "some text";\n\nawait Expect.That(subject).IsEqualTo("some text")\n  .Because("it is equal");\nawait Expect.That(subject).IsEqualTo("SOME TEXT").IgnoringCase()\n  .Because("we ignored the casing");\nawait Expect.That("a\\r\\nb").IsEqualTo("a\\nb").IgnoringNewlineStyle()\n  .Because("we ignored the newline style");\nawait Expect.That(subject).IsEqualTo("  some text").IgnoringLeadingWhiteSpace()\n  .Because("we ignored leading white-space");\nawait Expect.That(subject).IsEqualTo("some text \\t").IgnoringTrailingWhiteSpace()\n  .Because("we ignored trailing white-space");\nawait Expect.That(subject).IsEqualTo("SOME TEXT").Using(StringComparer.OrdinalIgnoreCase)\n  .Because("the comparer ignored the casing");\n'})}),"\n",(0,a.jsx)(n.h3,{id:"wildcards",children:"Wildcards"}),"\n",(0,a.jsx)(n.p,{children:"You can also compare strings using wildcards:"}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'string subject = "some text";\n\nawait Expect.That(subject).IsEqualTo("*me tex?").AsWildcard();\n'})}),"\n",(0,a.jsxs)(n.p,{children:["When using ",(0,a.jsx)(n.code,{children:"AsWildcard"}),", the following wildcard specifiers are supported:"]}),"\n",(0,a.jsxs)(n.table,{children:[(0,a.jsx)(n.thead,{children:(0,a.jsxs)(n.tr,{children:[(0,a.jsx)(n.th,{children:"Wildcard specifier"}),(0,a.jsx)(n.th,{children:"Matches"})]})}),(0,a.jsxs)(n.tbody,{children:[(0,a.jsxs)(n.tr,{children:[(0,a.jsx)(n.td,{children:"* (asterisk)"}),(0,a.jsx)(n.td,{children:"Zero or more characters"})]}),(0,a.jsxs)(n.tr,{children:[(0,a.jsx)(n.td,{children:"? (question mark)"}),(0,a.jsx)(n.td,{children:"Exactly one character"})]})]})]}),"\n",(0,a.jsx)(n.h3,{id:"regular-expressions",children:"Regular expressions"}),"\n",(0,a.jsxs)(n.p,{children:["You can also compare strings\nusing ",(0,a.jsx)(n.a,{href:"https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expressions",children:"regular expressions"}),":"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'string subject = "some text";\n\nawait Expect.That(subject).IsEqualTo("(.*)xt").AsRegex();\n'})}),"\n",(0,a.jsxs)(n.p,{children:["The regex comparison uses the following ",(0,a.jsxs)(n.a,{href:"https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regexoptions?view=net-8.0#fields",children:["\n",(0,a.jsx)(n.code,{children:"options"})]}),":"]}),"\n",(0,a.jsxs)(n.ul,{children:["\n",(0,a.jsxs)(n.li,{children:[(0,a.jsx)(n.code,{children:"Multiline"})," (always)"]}),"\n",(0,a.jsxs)(n.li,{children:[(0,a.jsx)(n.code,{children:"IgnoreCase"})," (if the ",(0,a.jsx)(n.code,{children:"IgnoringCase"})," method is also used)"]}),"\n"]}),"\n",(0,a.jsx)(n.h2,{id:"one-of",children:"One of"}),"\n",(0,a.jsxs)(n.p,{children:["You can verify, that the ",(0,a.jsx)(n.code,{children:"string"})," is one of many alternatives.",(0,a.jsx)(n.br,{}),"\n","This expectation can be configured to ignore case, ignore newline style, ignoring leading or trailing white-space, or\nuse a custom ",(0,a.jsx)(n.code,{children:"IEqualityComparer<string>"}),":"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'string subject = "some";\n\nawait Expect.That(subject).IsOneOf("none", "some", "many");\nawait Expect.That(subject).IsOneOf("NONE", "SOME", "MANY").IgnoringCase()\n  .Because("we ignored the casing");\nawait Expect.That(subject).IsOneOf("NONE", "SOME", "MANY").Using(StringComparer.OrdinalIgnoreCase)\n  .Because("the comparer ignored the casing");\n'})}),"\n",(0,a.jsx)(n.h2,{id:"null-empty-or-white-space",children:"Null, empty or white-space"}),"\n",(0,a.jsxs)(n.p,{children:["You can verify, that the ",(0,a.jsx)(n.code,{children:"string"})," is null, empty or contains only whitespace:"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'string? subject = null;\n\nawait Expect.That(subject).IsNull();\nawait Expect.That("foo").IsNotNull();\n\nawait Expect.That("").IsEmpty();\nawait Expect.That("foo").IsNotEmpty()\n  .Because("the string is not empty");\n\nawait Expect.That(subject).IsNullOrEmpty();\nawait Expect.That("foo").IsNotNullOrEmpty();\nawait Expect.That(subject).IsNullOrWhiteSpace();\nawait Expect.That("foo").IsNotNullOrWhiteSpace();\n'})}),"\n",(0,a.jsx)(n.h2,{id:"length",children:"Length"}),"\n",(0,a.jsxs)(n.p,{children:["You can verify, that the ",(0,a.jsx)(n.code,{children:"string"})," has the expected length:"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'string subject = "some value";\n\nawait Expect.That(subject).HasLength().EqualTo(10);\nawait Expect.That(subject).HasLength().NotEqualTo(9);\n'})}),"\n",(0,a.jsx)(n.h2,{id:"string-start--end",children:"String start / end"}),"\n",(0,a.jsxs)(n.p,{children:["You can verify, that the ",(0,a.jsx)(n.code,{children:"string"})," starts or ends with a given string.",(0,a.jsx)(n.br,{}),"\n","These expectations can be configured to ignore case, ignore newline style, ignoring leading or trailing white-space, or\nuse a custom ",(0,a.jsx)(n.code,{children:"IEqualityComparer<string>"}),":"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'string subject = "some text";\n\nawait Expect.That(subject).StartsWith("some");\nawait Expect.That(subject).StartsWith("SOME").IgnoringCase()\n  .Because("we ignored the casing");\nawait Expect.That(subject).StartsWith("SOME").Using(StringComparer.OrdinalIgnoreCase)\n  .Because("the comparer ignored the casing");\n\nawait Expect.That(subject).EndsWith("text");\nawait Expect.That(subject).EndsWith("TEXT").IgnoringCase()\n  .Because("we ignored the casing");\nawait Expect.That(subject).EndsWith("TEXT").Using(StringComparer.OrdinalIgnoreCase)\n  .Because("the comparer ignored the casing");\n'})}),"\n",(0,a.jsx)(n.h2,{id:"contains",children:"Contains"}),"\n",(0,a.jsxs)(n.p,{children:["You can verify, that the ",(0,a.jsx)(n.code,{children:"string"})," contains a given substring.",(0,a.jsx)(n.br,{}),"\n","These expectations can be configured to ignore case, ignore newline style, ignoring leading or trailing white-space, or\nuse a custom ",(0,a.jsx)(n.code,{children:"IEqualityComparer<string>"}),":"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'string subject = "some text";\n\nawait Expect.That(subject).Contains("me");\nawait Expect.That(subject).Contains("ME").IgnoringCase()\n  .Because("we ignored the casing");\nawait Expect.That(subject).Contains("ME").Using(StringComparer.OrdinalIgnoreCase)\n  .Because("the comparer ignored the casing");\n'})}),"\n",(0,a.jsx)(n.p,{children:"You can also specify, how often the substring should be found:"}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'string subject = "In this text in between the word an investigator should find the word \'IN\' multiple times.";\n\nawait Expect.That(subject).Contains("in").AtLeast(2)\n  .Because("\'in\' can be found 3 times");\nawait Expect.That(subject).Contains("in").Exactly(3)\n  .Because("\'in\' can be found 3 times");\nawait Expect.That(subject).Contains("in").AtMost(4)\n  .Because("\'in\' can be found 3 times");\nawait Expect.That(subject).Contains("in").Between(1).And(5)\n  .Because("\'in\' can be found 3 times");\n'})}),"\n",(0,a.jsx)(n.h2,{id:"character-casing",children:"Character casing"}),"\n",(0,a.jsxs)(n.p,{children:["You can verify, that the characters in a ",(0,a.jsx)(n.code,{children:"string"})," are all upper or lower cased:"]}),"\n",(0,a.jsx)(n.pre,{children:(0,a.jsx)(n.code,{className:"language-csharp",children:'await Expect.That("1ST PLACE").IsUpperCased()\n  .Because("it contains no lowercase characters");\nawait Expect.That("1st PLACE").IsNotUpperCased()\n  .Because("it contains at least one lowercase characters");\n\nawait Expect.That("1st place").IsLowerCased()\n  .Because("it contains no uppercase characters");\nawait Expect.That("1st PLACE").IsNotLowerCased()\n  .Because("it contains at least one uppercase characters");\n'})})]})}function d(e={}){const{wrapper:n}={...(0,i.R)(),...e.components};return n?(0,a.jsx)(n,{...e,children:(0,a.jsx)(h,{...e})}):h(e)}},8453:(e,n,t)=>{t.d(n,{R:()=>r,x:()=>c});var s=t(6540);const a={},i=s.createContext(a);function r(e){const n=s.useContext(i);return s.useMemo((function(){return"function"==typeof e?e(n):{...n,...e}}),[n,e])}function c(e){let n;return n=e.disableParentContext?"function"==typeof e.components?e.components(a):e.components||a:r(e.components),s.createElement(i.Provider,{value:n},e.children)}}}]);