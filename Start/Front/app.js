const template = document.createElement('template');
template.innerHTML = `
  <style>
   :host{
     display:flex;
     justify-content:space-around;
     align-items:center;
   }
  </style>
  <my-switch position='off'></my-switch>
  <my-lamp></my-lamp>
`;
customElements.define(
  'my-app',
  class extends HTMLElement {
    constructor() {
      super();
      this._shadowRoot = this.attachShadow({ mode: 'open' });
      this._shadowRoot.appendChild(template.content.cloneNode(true));
    }

    connectedCallback() {}
  }
);
