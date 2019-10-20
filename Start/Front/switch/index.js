import switchOn from './switch-on.svg';
import switchOff from './switch-off.svg';

const template = document.createElement('template');
template.innerHTML = `
  <style>
  img {
    width: 300px;
  }
  </style>
  <img src="${switchOff}"/>
`;

customElements.define(
  'my-switch',
  class extends HTMLElement {
    static get observedAttributes() {
      return ['position'];
    }

    set position(value) {
      if (value === 'on') {
        this.setAttribute('position', value);
        this.$img.src = switchOn;
      } else if (value === 'off') {
        this.setAttribute('position', value);
        this.$img.src = switchOff;
      }
    }

    get position() {
      return this.getAttribute('position');
    }

    _togglePosition() {
      this.position = this.position === 'on' ? 'off' : 'on';
    }

    _positionToBoolean() {
      return this.position === 'on' ? true : false;
    }

    _getSvgUrlFromPosition(position) {
      return position === 'on' ? switchOn : switchOff;
    }

    _render() {
      console.log('render');
      this.$img.src = this._getSvgUrlFromPosition(this.position);
      console.log(this.$img);
    }

    constructor() {
      super();

      this._shadowRoot = this.attachShadow({ mode: 'open' });
      this._shadowRoot.appendChild(template.content.cloneNode(true));

      this.$img = this._shadowRoot.querySelector('img');
    }

    connectedCallback() {
      if (!this.hasAttribute('position')) {
        this.position = 'off';
      }

      this._shadowRoot.querySelector('img').onclick = () => {
        fetch(
          `https://localhost:5011?switchPosition=${!this._positionToBoolean()}`
        ).then(response => {
          if (response.status === 200) {
            this._togglePosition();
          }
        });
      };

      this._render();
    }

    attributeChangedCallback(name, oldValue, newValue) {
      console.log(name, oldValue, newValue);
    }
  }
);
