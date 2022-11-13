"use strict";

const e = React.createElement;

class LikeButton extends React.Component {
  constructor(props) {
    super(props);
    this.state = { liked: false };
  }

  render() {
    if (this.state.liked) {
      return "You liked comment number " + this.props.commentId;
    }

    return e(
      "button",
      { onClick: () => this.setState({ liked: true }) },
      "Like"
    );
  }
}

// Find all DOM containers, and render Like buttons into them.
const domContainers = document.querySelectorAll("#like_button_container");
for (const domContainer of domContainers) {
  const commentId = +domContainer.dataset.commentid;
  const root = ReactDOM.createRoot(domContainer);
  root.render(e(LikeButton, { commentId: commentId }));
}
