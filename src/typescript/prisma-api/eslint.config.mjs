import tsEslint from 'typescript-eslint';
import prettierConfig from 'eslint-plugin-prettier/recommended';

export default tsEslint.config(
  {
    ignores: [
      'node_modules',
      'dist',
      'eslint.config.mjs',
    ],
  },
  {
    extends: [
      tsEslint.configs.recommended,
    ],
    rules: {
      '@typescript-eslint/interface-name-prefix': 'off',
      '@typescript-eslint/explicit-function-return-type': 'off',
      '@typescript-eslint/explicit-module-boundary-types': 'off',
      '@typescript-eslint/no-explicit-any': 'off',
    }
  },
  prettierConfig,
);
