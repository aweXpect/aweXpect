"use strict";(self.webpackChunkpages=self.webpackChunkpages||[]).push([[479],{2429:(e,t,n)=>{n.r(t),n.d(t,{assets:()=>o,contentTitle:()=>c,default:()=>h,frontMatter:()=>r,metadata:()=>s,toc:()=>l});const s=JSON.parse('{"id":"expectations/string","title":"String","description":"Describes the possible expectations for strings.","source":"@site/docs/expectations/string.md","sourceDirName":"expectations","slug":"/expectations/string","permalink":"/aweXpect/docs/expectations/string","draft":false,"unlisted":false,"editUrl":"https://github.com/aweXpect/aweXpect/tree/main/Docs/pages/docs/expectations/string.md","tags":[],"version":"current","sidebarPosition":2,"frontMatter":{"sidebar_position":2},"sidebar":"tutorialSidebar","previous":{"title":"Boolean","permalink":"/aweXpect/docs/expectations/boolean"},"next":{"title":"Number","permalink":"/aweXpect/docs/expectations/number"}}');var a=n(4848),i=n(8453);const r={sidebar_position:2},c="String",o={},l=[{value:"Equality",id:"equality",level:2},{value:"Wildcards",id:"wildcards",level:3},{value:"Regular expressions",id:"regular-expressions",level:3},{value:"One of",id:"one-of",level:2},{value:"Null, empty or white-space",id:"null-empty-or-white-space",level:2},{value:"String start / end",id:"string-start--end",level:2},{value:"Contains",id:"contains",level:2},{value:"Character casing",id:"character-casing",level:2}];function d(e){const t={a:"a",br:"br",code:"code",h1:"h1",h2:"h2",h3:"h3",header:"header",li:"li",p:"p",pre:"pre",table:"table",tbody:"tbody",td:"td",th:"th",thead:"thead",tr:"tr",ul:"ul",...(0,i.R)(),...e.components};return(0,a.jsxs)(a.Fragment,{children:[(0,a.jsx)(t.header,{children:(0,a.jsx)(t.h1,{id:"string",children:"String"})}),"\n",(0,a.jsx)(t.p,{children:"Describes the possible expectations for strings."}),"\n",(0,a.jsx)(t.h2,{id:"equality",children:"Equality"}),"\n",(0,a.jsxs)(t.p,{children:["You can verify, that the ",(0,a.jsx)(t.code,{children:"string"})," is equal to another one.",(0,a.jsx)(t.br,{}),"\n","This expectation can be configured to ignore case, or use a custom ",(0,a.jsx)(t.code,{children:"IEqualityComparer<string>"}),":"]}),"\n",(0,a.jsx)(t.pre,{children:(0,a.jsx)(t.code,{className:"language-csharp",children:'string subject = "some text";\n\nawait Expect.That(subject).Should().Be("some text")\n  .Because("it is equal");\nawait Expect.That(subject).Should().Be("SOME TEXT").IgnoringCase()\n  .Because("we ignored the casing");\nawait Expect.That(subject).Should().Be("SOME TEXT").Using(StringComparer.OrdinalIgnoreCase)\n  .Because("the comparer ignored the casing");\n'})}),"\n",(0,a.jsx)(t.h3,{id:"wildcards",children:"Wildcards"}),"\n",(0,a.jsx)(t.p,{children:"You can also compare strings using wildcards:"}),"\n",(0,a.jsx)(t.pre,{children:(0,a.jsx)(t.code,{className:"language-csharp",children:'string subject = "some text";\n\nawait Expect.That(subject).Should().Be("*me tex?").AsWildcard();\n'})}),"\n",(0,a.jsxs)(t.p,{children:["When using ",(0,a.jsx)(t.code,{children:"AsWildcard"}),", the following wildcard specifiers are supported:"]}),"\n",(0,a.jsxs)(t.table,{children:[(0,a.jsx)(t.thead,{children:(0,a.jsxs)(t.tr,{children:[(0,a.jsx)(t.th,{children:"Wildcard specifier"}),(0,a.jsx)(t.th,{children:"Matches"})]})}),(0,a.jsxs)(t.tbody,{children:[(0,a.jsxs)(t.tr,{children:[(0,a.jsx)(t.td,{children:"* (asterisk)"}),(0,a.jsx)(t.td,{children:"Zero or more characters"})]}),(0,a.jsxs)(t.tr,{children:[(0,a.jsx)(t.td,{children:"? (question mark)"}),(0,a.jsx)(t.td,{children:"Exactly one character"})]})]})]}),"\n",(0,a.jsx)(t.h3,{id:"regular-expressions",children:"Regular expressions"}),"\n",(0,a.jsxs)(t.p,{children:["You can also compare strings using ",(0,a.jsx)(t.a,{href:"https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expressions",children:"regular expressions"}),":"]}),"\n",(0,a.jsx)(t.pre,{children:(0,a.jsx)(t.code,{className:"language-csharp",children:'string subject = "some text";\n\nawait Expect.That(subject).Should().Be("(.*)xt").AsRegex();\n'})}),"\n",(0,a.jsxs)(t.p,{children:["The regex comparison uses the following ",(0,a.jsx)(t.a,{href:"https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regexoptions?view=net-8.0#fields",children:(0,a.jsx)(t.code,{children:"options"})}),":"]}),"\n",(0,a.jsxs)(t.ul,{children:["\n",(0,a.jsxs)(t.li,{children:[(0,a.jsx)(t.code,{children:"Multiline"})," (always)"]}),"\n",(0,a.jsxs)(t.li,{children:[(0,a.jsx)(t.code,{children:"IgnoreCase"})," (if the ",(0,a.jsx)(t.code,{children:"IgnoringCase"})," method is also used)"]}),"\n"]}),"\n",(0,a.jsx)(t.h2,{id:"one-of",children:"One of"}),"\n",(0,a.jsxs)(t.p,{children:["You can verify, that the ",(0,a.jsx)(t.code,{children:"string"})," is one of many alternatives.",(0,a.jsx)(t.br,{}),"\n","This expectation can be configured to ignore case, or use a custom ",(0,a.jsx)(t.code,{children:"IEqualityComparer<string>"}),":"]}),"\n",(0,a.jsx)(t.pre,{children:(0,a.jsx)(t.code,{className:"language-csharp",children:'string subject = "some";\n\nawait Expect.That(subject).Should().BeOneOf("none", "some", "many");\nawait Expect.That(subject).Should().BeOneOf("NONE", "SOME", "MANY").IgnoringCase()\n  .Because("we ignored the casing");\nawait Expect.That(subject).Should().BeOneOf("NONE", "SOME", "MANY").Using(StringComparer.OrdinalIgnoreCase)\n  .Because("the comparer ignored the casing");\n'})}),"\n",(0,a.jsx)(t.h2,{id:"null-empty-or-white-space",children:"Null, empty or white-space"}),"\n",(0,a.jsxs)(t.p,{children:["You can verify, that the ",(0,a.jsx)(t.code,{children:"string"})," is null, empty or contains only whitespace:"]}),"\n",(0,a.jsx)(t.pre,{children:(0,a.jsx)(t.code,{className:"language-csharp",children:'string? subject = null;\n\nawait Expect.That(subject).Should().BeNull();\nawait Expect.That("foo").Should().NotBeNull();\n\nawait Expect.That("").Should().BeEmpty();\nawait Expect.That("foo").Should().NotBeEmpty()\n  .Because("the string is not empty");\n\nawait Expect.That(subject).Should().BeNullOrEmpty();\nawait Expect.That("foo").Should().NotBeNullOrEmpty();\nawait Expect.That(subject).Should().BeNullOrWhiteSpace();\nawait Expect.That("foo").Should().NotBeNullOrWhiteSpace();\n'})}),"\n",(0,a.jsx)(t.h2,{id:"string-start--end",children:"String start / end"}),"\n",(0,a.jsxs)(t.p,{children:["You can verify, that the ",(0,a.jsx)(t.code,{children:"string"})," starts or ends with a given string.",(0,a.jsx)(t.br,{}),"\n","These expectations can be configured to ignore case, or use a custom ",(0,a.jsx)(t.code,{children:"IEqualityComparer<string>"}),":"]}),"\n",(0,a.jsx)(t.pre,{children:(0,a.jsx)(t.code,{className:"language-csharp",children:'string subject = "some text";\n\nawait Expect.That(subject).Should().StartWith("some");\nawait Expect.That(subject).Should().StartWith("SOME").IgnoringCase()\n  .Because("we ignored the casing");\nawait Expect.That(subject).Should().StartWith("SOME").Using(StringComparer.OrdinalIgnoreCase)\n  .Because("the comparer ignored the casing");\n\nawait Expect.That(subject).Should().EndWith("text");\nawait Expect.That(subject).Should().EndWith("TEXT").IgnoringCase()\n  .Because("we ignored the casing");\nawait Expect.That(subject).Should().EndWith("TEXT").Using(StringComparer.OrdinalIgnoreCase)\n  .Because("the comparer ignored the casing");\n'})}),"\n",(0,a.jsx)(t.h2,{id:"contains",children:"Contains"}),"\n",(0,a.jsxs)(t.p,{children:["You can verify, that the ",(0,a.jsx)(t.code,{children:"string"})," contains a given substring.",(0,a.jsx)(t.br,{}),"\n","These expectations can be configured to ignore case, or use a custom ",(0,a.jsx)(t.code,{children:"IEqualityComparer<string>"}),":"]}),"\n",(0,a.jsx)(t.pre,{children:(0,a.jsx)(t.code,{className:"language-csharp",children:'string subject = "some text";\n\nawait Expect.That(subject).Should().Contain("me");\nawait Expect.That(subject).Should().Contain("ME").IgnoringCase()\n  .Because("we ignored the casing");\nawait Expect.That(subject).Should().Contain("ME").Using(StringComparer.OrdinalIgnoreCase)\n  .Because("the comparer ignored the casing");\n'})}),"\n",(0,a.jsx)(t.p,{children:"You can also specify, how often the substring should be found:"}),"\n",(0,a.jsx)(t.pre,{children:(0,a.jsx)(t.code,{className:"language-csharp",children:'string subject = "In this text in between the word an investigator should find the word \'IN\' multiple times.";\n\nawait Expect.That(subject).Should().Contain("in").AtLeast(2)\n  .Because("\'in\' can be found 3 times");\nawait Expect.That(subject).Should().Contain("in").Exactly(3)\n  .Because("\'in\' can be found 3 times");\nawait Expect.That(subject).Should().Contain("in").AtMost(4)\n  .Because("\'in\' can be found 3 times");\nawait Expect.That(subject).Should().Contain("in").Between(1).And(5)\n  .Because("\'in\' can be found 3 times");\n'})}),"\n",(0,a.jsx)(t.h2,{id:"character-casing",children:"Character casing"}),"\n",(0,a.jsxs)(t.p,{children:["You can verify, that the characters in a ",(0,a.jsx)(t.code,{children:"string"})," are all upper or lower cased:"]}),"\n",(0,a.jsx)(t.pre,{children:(0,a.jsx)(t.code,{className:"language-csharp",children:'await Expect.That("1ST PLACE").Should().BeUpperCased()\n  .Because("it contains no lowercase characters");\nawait Expect.That("1st PLACE").Should().NotBeUpperCased()\n  .Because("it contains at least one lowercase characters");\n\nawait Expect.That("1st place").Should().BeLowerCased()\n  .Because("it contains no uppercase characters");\nawait Expect.That("1st PLACE").Should().NotBeLowerCased()\n  .Because("it contains at least one uppercase characters");\n'})})]})}function h(e={}){const{wrapper:t}={...(0,i.R)(),...e.components};return t?(0,a.jsx)(t,{...e,children:(0,a.jsx)(d,{...e})}):d(e)}},8453:(e,t,n)=>{n.d(t,{R:()=>r,x:()=>c});var s=n(6540);const a={},i=s.createContext(a);function r(e){const t=s.useContext(i);return s.useMemo((function(){return"function"==typeof e?e(t):{...t,...e}}),[t,e])}function c(e){let t;return t=e.disableParentContext?"function"==typeof e.components?e.components(a):e.components||a:r(e.components),s.createElement(i.Provider,{value:t},e.children)}}}]);