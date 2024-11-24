import React, {useEffect} from 'react';
import Layout from '@theme/Layout';

export default function Benchmarks() {
	useEffect(() => {
		const chartScript = document.createElement('script');
		chartScript.setAttribute('id', "benchmark-chart-script");
		chartScript.setAttribute('async', 'true');
		chartScript.setAttribute('src', 'js/Chart.min.js');
		document.getElementById("__docusaurus").appendChild(chartScript);
		const dataScript = document.createElement('script');
		dataScript.setAttribute('id', "benchmark-data-script");
		chartScript.setAttribute('async', 'true');
		dataScript.setAttribute('src', 'js/data.js');
		document.getElementById("__docusaurus").appendChild(dataScript);
		const benchmarkScript = document.createElement('script');
		benchmarkScript.setAttribute('src', 'js/benchmarks.js');
		document.getElementById("__docusaurus").appendChild(benchmarkScript);
	}, []);
	return (
		<Layout title="Benchmarks" description="Benchmarks Page">
			<div id="benchmarks-container">
			</div>
		</Layout>
	);
}
