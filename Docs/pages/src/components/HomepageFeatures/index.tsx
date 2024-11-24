import clsx from 'clsx';
import Heading from '@theme/Heading';
import styles from './styles.module.css';

type FeatureItem = {
  title: string;
  Svg: React.ComponentType<React.ComponentProps<'svg'>>;
  description: JSX.Element;
};

const FeatureList: FeatureItem[] = [
  {
    title: 'Async everything',
    Svg: require('@site/static/img/async.svg').default,
    description: (
      <>
        By using async assertions per default, we have a consistent API and other perks.
      </>
    ),
  },
  {
    title: 'Extensible',
    Svg: require('@site/static/img/extensibility.svg').default,
    description: (
      <>
        We added lots of extensibility points to allow you to build custom extensions.
      </>
    ),
  },
  {
    title: 'Performant',
    Svg: require('@site/static/img/speed.svg').default,
    description: (
      <>
        A focus on performance allows you to execute your tests as fast as possible.
      </>
    ),
  },
];

function Feature({title, Svg, description}: FeatureItem) {
  return (
    <div className={clsx('col col--4')}>
      <div className="text--center">
        <Svg className={styles.featureSvg} role="img" />
      </div>
      <div className="text--center padding-horiz--md">
        <Heading as="h3">{title}</Heading>
        <p>{description}</p>
      </div>
    </div>
  );
}

export default function HomepageFeatures(): JSX.Element {
  return (
    <section className={styles.features}>
      <div className="container">
        <div className="row">
          {FeatureList.map((props, idx) => (
            <Feature key={idx} {...props} />
          ))}
        </div>
      </div>
    </section>
  );
}
