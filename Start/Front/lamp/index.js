import bulbOffUrl from './light-bulb-off.svg';
import bulbOnUrl from './light-bulb-on.svg';

const template = document.createElement('template');
template.innerHTML = `
  <style>
    img {
      width: 500px;
    }
  </style>
  <img src="${bulbOffUrl}"/>
`;

customElements.define(
  'my-lamp',
  class extends HTMLElement {
    static get observedAttributes() {
      return ['position'];
    }

    set position(value) {
      if (value === 'on') {
        this.setAttribute('position', value);
        this.$img.src = bulbOnUrl;
      } else if (value === 'off') {
        this.setAttribute('position', value);
        this.$img.src = bulbOffUrl;
      }
    }

    get position() {
      return this.getAttribute('position');
    }

    _togglePosition() {
      this.position = this.position === 'on' ? 'off' : 'on';
    }

    _getSvgUrlFromPosition(position) {
      return position === 'on' ? bulbOnUrl : bulbOffUrl;
    }

    _render() {
      this.$img.src = this._getSvgUrlFromPosition(this.position);
    }

    constructor() {
      super();

      this._shadowRoot = this.attachShadow({ mode: 'open' });
      this._shadowRoot.appendChild(template.content.cloneNode(true));

      this.$img = this._shadowRoot.querySelector('img');
    }

    connectedCallback() {
      setInterval(() => {
        fetch('https://localhost:5001').then(response =>
          response
            .json()
            .then(({ position }) => (this.position = position.toLowerCase()))
        );
      }, 500);

      if (!this.hasAttribute('position')) {
        this.position = 'off';
      }

      this._render();
    }

    attributeChangedCallback(name, oldValue, newValue) {
      console.log(name, oldValue, newValue);
    }
  }
);
