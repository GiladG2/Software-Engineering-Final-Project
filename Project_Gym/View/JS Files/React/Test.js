// React component in EditableText.js
class EditableText extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      isEditable: false,
      text: 'This is your current training plan',
    };
  }

  toggleEdit = () => {
    this.setState({ isEditable: !this.state.isEditable });
  };

  handleChange = (event) => {
    this.setState({ text: event.target.value });
  };

  render() {
    return (
      <div>
       <h1>hi from react</h1>
      </div>
    );
  }
}

// Export the component if needed
export default EditableText;
