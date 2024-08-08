const esbuild = require('esbuild');

// general
esbuild.build({
  entryPoints: ['ts/menu.ts'], // Adjust the path to your TypeScript file,
  bundle: true,
  outfile: 'js/menu.js', // Adjust the output file path as needed
  platform: 'browser',
  format: 'iife',
  globalName: 'ui',
  target: ['chrome58', 'firefox57', 'safari11', 'edge16'],
  minify: true,
}).catch(() => process.exit(1));

esbuild.build({
    entryPoints: ['ts/objectEditor.ts'], // Adjust the path to your TypeScript file,
    bundle: true,
    outfile: 'js/objectEditor.js', // Adjust the output file path as needed
    platform: 'browser',
    format: 'iife',
    globalName: 'editors',
    target: ['es2015'],
    minify: true,
}).catch(() => process.exit(1));
  
esbuild.build({
  entryPoints: ['ts/participation/setup.ts'], // Adjust the path to your TypeScript file,
  bundle: true,
  outfile: 'js/participation/setup.js', // Adjust the output file path as needed
  platform: 'browser',
  format: 'iife',
  globalName: 'admin',
  target: ['es2015'],
  minify: true,
}).catch(() => process.exit(1));
