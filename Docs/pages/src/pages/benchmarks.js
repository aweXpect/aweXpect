﻿import React, {useEffect} from 'react';
import Layout from '@theme/Layout';

export default function Benchmarks() {
	useEffect(() => {
		const chartScript = document.createElement('script');
		chartScript.setAttribute('src', 'js/Chart.min.js');
		document.getElementById("__docusaurus").appendChild(chartScript);
		const dataScript = document.createElement('script');
		dataScript.setAttribute('src', 'js/data.js');
		document.getElementById("__docusaurus").appendChild(dataScript);
		const benchmarkScript = document.createElement('script');
		benchmarkScript.setAttribute('src', 'js/benchmarks.js');
		document.getElementById("__docusaurus").appendChild(benchmarkScript);
	}, []);
	return (
		<Layout title="Benchmarks" description="Benchmarks Page">
			<div id="benchmarks-container"
				style={{
					margin: 'auto',
					'margin-top': '20px',
					width: '60%',
				}}>
			</div>
		</Layout>
	);
}
