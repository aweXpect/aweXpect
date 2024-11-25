import {themes as prismThemes} from 'prism-react-renderer';
import type {Config} from '@docusaurus/types';
import type * as Preset from '@docusaurus/preset-classic';

// This runs in Node.js - Don't use client-side code here (browser APIs, JSX...)

const config: Config = {
  title: 'aweXpect',
  tagline: 'Assert unit tests in natural language using awesome expectations.',
  favicon: 'img/favicon.ico',

  // Set the production url of your site here
  url: 'https://aweXpect.github.io',
  // Set the /<baseUrl>/ pathname under which your site is served
  // For GitHub pages deployment, it is often '/<projectName>/'
  baseUrl: '/aweXpect/',

  // GitHub pages deployment config.
  // If you aren't using GitHub pages, you don't need these.
  organizationName: 'aweXpect', // Usually your GitHub org/user name.
  projectName: 'aweXpect', // Usually your repo name.
  trailingSlash: false,

  onBrokenLinks: 'throw',
  onBrokenMarkdownLinks: 'warn',

  // Even if you don't use internationalization, you can use this field to set
  // useful metadata like html lang. For example, if your site is Chinese, you
  // may want to replace "en" with "zh-Hans".
  i18n: {
    defaultLocale: 'en',
    locales: ['en'],
  },

  presets: [
    [
      'classic',
      {
        docs: {
          sidebarPath: './sidebars.ts',
          // Please change this to your repo.
          // Remove this to remove the "edit this page" links.
          editUrl:
            'https://github.com/facebook/docusaurus/tree/main/packages/create-docusaurus/templates/shared/',
        },
        blog: {
          showReadingTime: true,
          feedOptions: {
            type: ['rss', 'atom'],
            xslt: true,
          },
          // Please change this to your repo.
          // Remove this to remove the "edit this page" links.
          editUrl:
            'https://github.com/facebook/docusaurus/tree/main/packages/create-docusaurus/templates/shared/',
          // Useful options to enforce blogging best practices
          onInlineTags: 'warn',
          onInlineAuthors: 'warn',
          onUntruncatedBlogPosts: 'warn',
        },
        theme: {
          customCss: './src/css/custom.css',
        },
      } satisfies Preset.Options,
    ],
  ],

  themeConfig: {
    // Replace with your project's social card
    image: 'img/docusaurus-social-card.jpg',
    navbar: {
      title: 'aweXpect',
      logo: {
        alt: 'aweXpect logo',
        src: 'img/logo_256x256.png',
      },
      items: [
        {
          type: 'docSidebar',
          sidebarId: 'tutorialSidebar',
          position: 'left',
          label: 'Documentation',
        },
        {
          to: '/benchmarks',
          label: 'Benchmarks',
          position: 'left'
        },
        {
          to: '/blog',
          label: 'Blog',
          position: 'right'
        },
      ],
    },
    footer: {
      style: 'dark',
      links: [
        {
          title: 'Docs',
          items: [
            {
              label: 'Documentation',
              to: '/docs/getting-started',
            },
            {
              label: 'Benchmarks',
              to: '/benchmarks',
            },
            {
              label: "Blog",
              to: "/blog"
            },
          ],
        },
        {
          title: 'Code Quality',
          items: [
            {
              label: 'SonarQube',
              href: "https://sonarcloud.io/project/overview?id=aweXpect_aweXpect"
            },
            {
              label: 'Stryker Mutator',
              href: 'https://dashboard.stryker-mutator.io/reports/github.com/aweXpect/aweXpect/main#mutant',
            },
          ],
        },
        {
          title: 'More',
          items: [
            {
              label: 'GitHub',
              href: 'https://github.com/facebook/docusaurus',
            },
            {
              label: 'NuGet',
              href: 'https://www.nuget.org/packages/aweXpect',
            },
          ],
        },
      ],
      copyright: `Copyright © ${new Date().getFullYear()} Valentin Breuß`,
    },
    prism: {
      theme: prismThemes.github,
      darkTheme: prismThemes.dracula,
    },
  } satisfies Preset.ThemeConfig,
};

export default config;
