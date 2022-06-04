{
	"name": "my-awesome-package",
  "version": "0.0.0",
	"private": true,
	"scripts": {
    "dev": "vite",
    "build": "tsc && vite build",
    "preview": "vite preview",
    "test": "vitest",
    "test:ui": "vitest --ui",
		"lint": "xo --fix",
		"version": "./build-docs && git add docs",
		"release": "np"
	},
	"devDependencies": {
		"np": "*",
		"xo": "*",
    "vite": "*",
    "vitest": "*"
	},
	"np": {
		"yarn": false,
		"contents": "dist"
	},
	"xo": {
		"space": 2,
    "semicolon": false
	}
}
{


  "scripts": {

  },
  "devDependencies": {
    "@types/node": "^17.0.23",
    "@vitest/ui": "^0.8.0",
    "happy-dom": "^2.50.0",
    "jsdom": "^19.0.0",
    "pathe": "^0.2.0",
    "taze": "^0.5.0",
    "typescript": "^4.6.3",
    "vite": "^2.8.6",
    "vitest": "^0.8.0"
  },
  "stackblitz": {
    "startCommand": "npm run test:ui"
  }
}
