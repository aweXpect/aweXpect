import React, {useEffect} from 'react';
import Layout from '@theme/Layout';

export default function Benchmarks() {
	useEffect(() => {
		const chartScript = document.createElement('script');
		chartScript.setAttribute('src', 'js/chart.umd.js');
		document.getElementById("__docusaurus").appendChild(chartScript);
		const dataScript = document.createElement('script');
		dataScript.setAttribute('src', 'js/limited-data.js');
		document.getElementById("__docusaurus").appendChild(dataScript);
		const benchmarkScript = document.createElement('script');
		benchmarkScript.setAttribute('src', 'js/benchmarks.js');
		document.getElementById("__docusaurus").appendChild(benchmarkScript);
	}, []);
	return (
		<Layout title="Benchmarks" description="Benchmarks Page">
			<div id="benchmarks-container">
			</div>
			<a id="benchmarks-view-link" href="/benchmarks-full">View full benchmarks</a>
		</Layout>
	);
}
